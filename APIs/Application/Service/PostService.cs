using Application.Common;
using Application.InterfaceService;
using Application.Util;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppConfiguration _appConfiguration;
        private readonly ICurrentTime _currentTime;
        private readonly IClaimService _claimService;
        public PostService(IUnitOfWork unitOfWork, IMapper mapper, AppConfiguration appConfiguration, ICurrentTime currentTime
            ,  IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
            _currentTime = currentTime;
            _claimService = claimService;
        }
        public async Task<bool> BanPost(Guid postId)
        {
           var post= await _unitOfWork.PostRepository.GetByIdAsync(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            _unitOfWork.PostRepository.SoftRemove(post);
            return await _unitOfWork.SaveChangeAsync()>0;
        }
    }
}
