﻿using AutoMapper;
using VidBox.DataAccess.Interfaces.Videos;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Videos;
using VidBox.Domain.Exceptions.Categories;
using VidBox.Domain.Exceptions.Videos;
using VidBox.Service.Common.Helpers;
using VidBox.Service.Dtos.Videos;
using VidBox.Service.Interfaces.Common;
using VidBox.Service.Interfaces.Videos;

namespace VidBox.Service.Services.Videos;

public class VideoService : IVideoService
{
    private readonly IFileService _fileService;
    private readonly IVideoRepository _videoRepository;
    private readonly IPaginator _paginator;
  

    public VideoService(IVideoRepository videoRepository,
        IFileService fileService,
        IPaginator paginator
        )
    {
        this._fileService = fileService;
        this._videoRepository = videoRepository;
        this._paginator = paginator;
    }
    public Task<long> CountAsync()
    {
        var videoCount = _videoRepository.CountAsync();
        return videoCount;
    }

    public async Task<bool> CreateAsync(VideoCreateDto dto)
    {
        string videoPath = await _fileService.UploadVideoAsync(dto.VideoPath);
        //Video video = _mapper.Map<Video>(dto);
        Video video = new Video();
        video.CategoryId = dto.CategoryId;
        video.Name = dto.Name;
        video.Description = dto.Description;
        video.VideoPath = videoPath;
        video.CreatedAt = TimeHelper.GetDateTime();
        video.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _videoRepository.CreateAsync(video);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long videoId)
    {
        var video = await _videoRepository.GetByIdAsync(videoId);
        if (video is null) throw new CategoryNotFoundException();

        var result = await _fileService.DeleteVideoAsync(video.VideoPath);
        if (result == false) throw new VideoNotFoundException();

        var dbResult = await _videoRepository.DeleteAsync(videoId);

        return dbResult > 0;
    }

    public async Task<IList<Video>> GetAllAsync(PaginationParams @params)
    {
        var video = await _videoRepository.GetAllAsync(@params);
        var count = await _videoRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return video;
    }

    public Task<Video> GetAllCategoryId(long categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Video?> GetByIdAsync(long id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video is null) throw new VideoNotFoundException();

        return video;
    }

    public async Task<IList<Video>> SearchAsync(string search)
    {
        var video = await _videoRepository.SearchAsync(search);

        return video;
    }

    public async Task<bool> UpdateAsync(long videoId, VideoUpdateDto dto)
    {
        var video = await _videoRepository.GetByIdAsync(videoId);
        if (video is null) throw new VideoNotFoundException();
        video.CategoryId = dto.CategoryId;
        video.Name = dto.Name;
        video.Description = dto.Description;

        video.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _videoRepository.UpdateAsync(videoId, video);

        return dbResult > 0;
    }
}
