using MVCForum.Domain.DomainModel;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using System;
using System.Web.Http;
using MVCForum.Website.CustomAttributes;
using System.Web.Routing;
using System.Net.Http;
using System.Linq;

namespace MVCForum.Website.Controllers
{
    public class SiteApiController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMembershipService _membershipService;
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;

        public SiteApiController(ICategoryService categoryService, IUnitOfWorkManager unitOfWorkManager, 
            IMembershipService membershipService, ITopicService topicService, IPostService postService)
        {
            _categoryService = categoryService;
            _unitOfWorkManager = unitOfWorkManager;
            _membershipService = membershipService;
            _topicService = topicService;
            _postService = postService;
        }
        
        [HttpGet]
        public IHttpActionResult Tofu()
        {
            return Ok("Tofu");
        }

        [WarAuthorize]
        [HttpGet]
        public IHttpActionResult ScrapWar(string warName)
        {
            var catId = new Guid("B021A3BE-02C9-46B6-BCC8-A53900AB0A6A");
            var category = _categoryService.Get(catId);

            using (var unitOfWork = _unitOfWorkManager.NewUnitOfWork())
            {
                _topicService.GetAll().Where(t => t.Category == category && t.Name == warName).ToList().ForEach(t =>
                    t.Solved = true
                    );

                // Save the changes
                unitOfWork.SaveChanges();
                unitOfWork.Commit();
            }

            return Ok();
        }
        
        [WarAuthorize]
        [HttpGet]
        public IHttpActionResult CreateWarTopic(string name, string warName, string warDetails)
        {
            var catId = new Guid("B021A3BE-02C9-46B6-BCC8-A53900AB0A6A");
            var category = _categoryService.Get(catId);
            using (var unitOfWork = _unitOfWorkManager.NewUnitOfWork())
            {
                // Create the topic model
                var topic = new Topic
                {
                    Name = warName,
                    Category = category,
                    User = _membershipService.GetUser("Clyde en Marland")
                };

                // Create the topic
                topic = _topicService.Add(topic);

                // Save the changes
                unitOfWork.SaveChanges();

                // Now create and add the post to the topic
                var topicPost = _topicService.AddLastPost(topic, warDetails);
                // Save the changes
                unitOfWork.SaveChanges();
                unitOfWork.Commit();

            }
            return Ok();
        }

        public void Execute(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}
