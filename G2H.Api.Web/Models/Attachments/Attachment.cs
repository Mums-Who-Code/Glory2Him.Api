﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using G2H.Api.Web.Models.Approvals;
using G2H.Api.Web.Models.Base;
using G2H.Api.Web.Models.Users;

namespace G2H.Api.Web.Models.Attachments
{
    public class Attachment : IKey, IApproval, IAudit
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public byte[] Contents { get; set; }
        public string ContentType { get; set; }
        public string Extension { get; set; }
        public string ExternalUrl { get; set; }
        public Guid ApprovalId { get; set; }
        public Approval Approval { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public ApplicationUser UpdatedByUser { get; set; }
    }
}
