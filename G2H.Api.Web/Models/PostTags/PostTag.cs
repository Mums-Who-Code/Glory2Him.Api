﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using G2H.Api.Web.Models.Base;
using G2H.Api.Web.Models.Posts;
using G2H.Api.Web.Models.Statuses;
using G2H.Api.Web.Models.Tags;

namespace G2H.Api.Web.Models.PostTags
{
    public class PostTag : IStatus, IAudit
    {
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }

        public StatusId StatusId { get; set; }
        public Status Status { get; set; }

        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}