﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using EFxceptions.Identity;
using G2H.Api.Web.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace G2H.Api.Web.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsIdentityContext<ApplicationUser, ApplicationRole, Guid>, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddApprovalReferences(modelBuilder);
            AddApprovalUserReferences(modelBuilder);
            AddAttachmentReferences(modelBuilder);
            AddCommentCommentReferences(modelBuilder);
            AddCommentReactionReferences(modelBuilder);
            AddCommentReferences(modelBuilder);
            AddPostAttachmentReferences(modelBuilder);
            AddPostCommentReferences(modelBuilder);
            AddPostReactionReferences(modelBuilder);
            AddPostReferences(modelBuilder);
            AddPostTagReferences(modelBuilder);
            AddPostTypeReferences(modelBuilder);
            AddReactionReferences(modelBuilder);
            AddStatusReferences(modelBuilder);
            AddTagReferences(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration
                .GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }
    }
}
