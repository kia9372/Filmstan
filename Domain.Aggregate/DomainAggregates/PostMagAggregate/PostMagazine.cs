using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.PostMagAggregate
{
    public class PostMagazine : Aggregates, IAggregateMarker
    {
        #region BackingField
        private string _title;
        private string _desciption;
        private string _photo;
        private string _postContent;
        private string _downloadLink;
        private string _subtitleLink;
        private int _viewCount;
        private Guid _categoryId;
        private Guid _writeId;
        private DateTimeOffset _registerDate;
        #endregion
        #region Properties
        public string Title { get => _title; private set => SetWithNotify(value, ref _title); }
        public string Description { get => _desciption; private set => SetWithNotify(value, ref _desciption); }
        public string Photo { get => _photo; private set => SetWithNotify(value, ref _photo); }
        public string PostContent { get => _postContent; private set => SetWithNotify(value, ref _postContent); }
        public string DownloadLink { get => _downloadLink; private set => SetWithNotify(value, ref _downloadLink); }
        public string SubTitleLink { get => _subtitleLink; private set => SetWithNotify(value, ref _subtitleLink); }
        public int ViewCount { get => _viewCount; private set => SetWithNotify(value, ref _viewCount); }
        public Guid CategoryId { get => _categoryId; private set => SetWithNotify(value, ref _categoryId); }
        public Guid WriterId { get => _writeId; private set => SetWithNotify(value, ref _writeId); }
        public DateTimeOffset RegisterDate { get => _registerDate; private set => SetWithNotify(value, ref _registerDate); }
        public Category Category { get; set; }
        public User User { get; set; }
        #endregion
        public PostMagazine()
        {

        }
        public PostMagazine(string title, string description, string photo, string postContent, string downloadLink, string subTitleLink, Guid categoryId, Guid writeId)
        {
            SetProperties(title, description, photo, postContent, downloadLink, subTitleLink, categoryId, writeId);
        }
        #region SetValues
        public void SetProperties(string title, string description, string photo, string postContent, string downloadLink, string subTitleLink, Guid categoryId, Guid writeId)
        {
            Title = title;
            Description = description;
            Photo = photo;
            PostContent = postContent;
            DownloadLink = downloadLink;
            SubTitleLink = subTitleLink;
            CategoryId = categoryId;
            WriterId = writeId;
            RegisterDate = DateTimeOffset.UtcNow;
            IncreaseViewCount();
        }
        public void IncreaseViewCount()
        {
            ViewCount++;
        }
        #endregion

    }
}
