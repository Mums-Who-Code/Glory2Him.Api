﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using G2H.Api.Web.Tests.Acceptance.Models.Approvals;
using Xunit;

namespace G2H.Api.Web.Tests.Acceptance.Apis.Approvals
{
    public partial class ApprovalsApiTests
    {
        [Fact]
        public async Task ShouldPostApprovalAsync()
        {
            // given
            Approval randomApproval = CreateRandomApproval();
            Approval inputApproval = randomApproval;
            Approval expectedApproval = inputApproval;

            // when 
            await this.apiBroker.PostApprovalAsync(inputApproval);

            Approval actualApproval =
                await this.apiBroker.GetApprovalByIdAsync(inputApproval.Id);

            // then
            actualApproval.Should().BeEquivalentTo(expectedApproval);
            await this.apiBroker.DeleteApprovalByIdAsync(actualApproval.Id);
        }

        [Fact]
        public async Task ShouldGetAllApprovalsAsync()
        {
            // given
            List<Approval> randomApprovals = await CreateRandomApprovalsAsync();

            List<Approval> expectedApprovals = randomApprovals;

            // when
            List<Approval> actualApprovals = await this.apiBroker.GetAllApprovalsAsync();

            // then
            foreach (Approval expectedApproval in expectedApprovals)
            {
                Approval actualApproval = actualApprovals.Single(approval => approval.Id == expectedApproval.Id);
                actualApproval.Should().BeEquivalentTo(expectedApproval);
                await this.apiBroker.DeleteApprovalByIdAsync(actualApproval.Id);
            }
        }
    }
}
