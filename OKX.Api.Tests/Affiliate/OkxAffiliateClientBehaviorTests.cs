using ApiSharp.Throttling;
using OKX.Api.Affiliate;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Affiliate;

public class OkxAffiliateClientBehaviorTests
{
    [Fact]
    public async Task GetPerformanceSummaryAsync_SendsCustomWindowAndParsesAllCategories()
    {
        using var server = CreateServer("/api/v5/affiliate/performance/summary", "performance-summary.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetPerformanceSummaryAsync(new OkxAffiliatePerformanceSummaryRequest
        {
            PeriodType = OkxAffiliatePeriodType.Custom,
            Begin = 1775000000000,
            End = 1775086400000,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1777541513000, result.Data!.UpdateTimestamp);
        Assert.Equal(102, result.Data.InviteeCount);
        Assert.Equal(1756.287846940199989393m, result.Data.DepositAmount);
        Assert.Collection(
            result.Data.Details,
            detail => Assert.Equal(OkxAffiliateCommissionCategory.Spot, detail.CommissionCategory),
            detail => Assert.Equal(OkxAffiliateCommissionCategory.Derivative, detail.CommissionCategory),
            detail => Assert.Equal(OkxAffiliateCommissionCategory.Bsc, detail.CommissionCategory));

        var query = GetDecodedQuery(server);
        Assert.Contains("periodType=custom", query);
        Assert.Contains("begin=1775000000000", query);
        Assert.Contains("end=1775086400000", query);
    }

    [Fact]
    public async Task GetInviteeAsync_UsesCurrentFieldNamesAndPeriodVolume()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/detail", "invitee-detail.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetInviteeAsync(new OkxAffiliateInviteeDetailRequest
        {
            UserId = "835449167911924693",
            PeriodType = OkxAffiliatePeriodType.Last30Days,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(2, result.Data!.InviteeLevel);
        Assert.Equal(12.5m, result.Data.AccumulatedWithdrawalAmount);
        Assert.Equal(98.75m, result.Data.TotalVolume);
        Assert.Equal(1234.56m, result.Data.PeriodVolume);
        Assert.Null(result.Data.FirstTradeTime);

        var query = GetDecodedQuery(server);
        Assert.Contains("uid=835449167911924693", query);
        Assert.Contains("periodType=last_30d", query);
    }

    [Fact]
    public async Task GetInviteesAsync_SendsCurrentFiltersAndPreservesRootPagination()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/list", "invitee-list.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetInviteesAsync(new OkxAffiliateInviteeListRequest
        {
            Page = 2,
            Limit = 25,
            PeriodType = OkxAffiliatePeriodType.Custom,
            Begin = 1775000000000,
            End = 1775086400000,
            Keyword = "X2UWA2T89",
            CommissionCategory = OkxAffiliateCommissionCategory.Spot,
            OrderBy = OkxAffiliateSortField.Volume,
            OrderDirection = OkxAffiliateSortDirection.Ascending,
            KycStatus = OkxAffiliateKycStatus.Verified,
            SubAffiliateUserId = "668418489887292061",
            UserIds = ["835449167911924693", "835449167911924700"],
            JoinTimeBegin = 1775000000000,
            JoinTimeEnd = 1775086400000,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(5, result.Data!.TotalPages);
        var invitee = Assert.Single(result.Data.Items);
        Assert.Equal("835449167911924693", invitee.UserId);
        Assert.Equal(OkxAffiliateKycStatus.Verified, invitee.KycStatus);
        Assert.True(invitee.IsCompliant);

        var query = GetDecodedQuery(server);
        Assert.Contains("page=2", query);
        Assert.Contains("limit=25", query);
        Assert.Contains("periodType=custom", query);
        Assert.Contains("commissionCategory=SPOT", query);
        Assert.Contains("orderBy=vol", query);
        Assert.Contains("orderDir=asc", query);
        Assert.Contains("kycStatus=verified", query);
        Assert.Contains("subAffiliateUid=668418489887292061", query);
        Assert.Contains("uid=835449167911924693,835449167911924700", query);
        Assert.Contains("joinTimeBegin=1775000000000", query);
        Assert.Contains("joinTimeEnd=1775086400000", query);
    }

    [Fact]
    public async Task GetAffiliateLinksAsync_ParsesNullableCoInviterRate()
    {
        using var server = CreateServer("/api/v5/affiliate/link/list", "link-list.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetAffiliateLinksAsync(new OkxAffiliateLinkListRequest
        {
            LinkType = OkxAffiliateLinkType.Standard,
            LinkStatus = OkxAffiliateLinkStatus.Normal,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1, result.Data!.TotalPages);
        var link = Assert.Single(result.Data.Items);
        Assert.Equal(OkxAffiliateLinkType.Standard, link.LinkType);
        Assert.Null(link.CoInviterCommissionRate);
        Assert.Equal(0.5m, link.Commission24Hours);

        var query = GetDecodedQuery(server);
        Assert.Contains("linkType=standard", query);
        Assert.Contains("linkStatus=normal", query);
    }

    [Fact]
    public async Task GetCoInviterLinksAsync_ParsesCurrentComplianceFields()
    {
        using var server = CreateServer("/api/v5/affiliate/co-inviter/list", "co-inviter-list.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetCoInviterLinksAsync(new OkxAffiliateCoInviterLinkListRequest
        {
            LinkStatus = OkxAffiliateLinkStatus.Normal,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var link = Assert.Single(result.Data!.Items);
        Assert.Equal("partner campaign", link.Note);
        Assert.Equal("valid", link.ChannelAssessmentStatus);
        Assert.Equal("valid", link.InviterChannelStatus);
        Assert.Equal("valid", link.CoInviterChannelStatus);
        Assert.True(link.IsCompliant);
    }

    [Fact]
    public async Task GetSubAffiliatesAsync_SendsSortingAndParsesLifetimeMetrics()
    {
        using var server = CreateServer("/api/v5/affiliate/sub-affiliate/list", "sub-affiliate-list.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetSubAffiliatesAsync(new OkxAffiliateSubAffiliateListRequest
        {
            Keyword = "668418489887292061",
            CommissionCategory = OkxAffiliateCommissionCategory.Derivative,
            OrderBy = OkxAffiliateSortField.Rebate,
            OrderDirection = OkxAffiliateSortDirection.Descending,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var subAffiliate = Assert.Single(result.Data!.Items);
        Assert.Equal("668418489887292061", subAffiliate.UserId);
        Assert.Equal(2, subAffiliate.Level);
        Assert.Equal(3618.561430m, subAffiliate.TotalVolume);

        var query = GetDecodedQuery(server);
        Assert.Contains("commissionCategory=DERIVATIVE", query);
        Assert.Contains("orderBy=rebate", query);
        Assert.Contains("orderDir=desc", query);
    }

    [Fact]
    public async Task PaginatedEndpoint_PreservesTopLevelServerErrors()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/list", "error.json");
        var client = CreateClient(server);

        var result = await client.Affiliate.GetInviteesAsync();

        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Contains("50014", result.Error.ToString());
    }

    [Fact]
    public async Task CustomPeriod_RequiresBothBounds()
    {
        using var server = CreateServer("/api/v5/affiliate/performance/summary", "performance-summary.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Affiliate.GetPerformanceSummaryAsync(new OkxAffiliatePerformanceSummaryRequest
        {
            PeriodType = OkxAffiliatePeriodType.Custom,
            Begin = 1775000000000,
        }));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task InviteeDetail_RejectsUnsupportedCustomPeriod()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/detail", "invitee-detail.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Affiliate.GetInviteeAsync(new OkxAffiliateInviteeDetailRequest
        {
            UserId = "835449167911924693",
            PeriodType = OkxAffiliatePeriodType.Custom,
        }));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task InviteeList_RejectsJoinTimeRangeOverNinetyDays()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/list", "invitee-list.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Affiliate.GetInviteesAsync(new OkxAffiliateInviteeListRequest
        {
            JoinTimeBegin = 0,
            JoinTimeEnd = 91L * 24L * 60L * 60L * 1000L,
        }));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task InviteeList_RejectsMoreThanOneHundredExactUids()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/list", "invitee-list.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Affiliate.GetInviteesAsync(new OkxAffiliateInviteeListRequest
        {
            UserIds = Enumerable.Range(1, 101).Select(x => x.ToString()),
        }));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task InviteeList_RejectsCustomAndJoinTimeStartsOlderThanOneHundredEightyDays()
    {
        using var server = CreateServer("/api/v5/affiliate/invitee/list", "invitee-list.json");
        var client = CreateClient(server);
        var oldStart = DateTimeOffset.UtcNow.AddDays(-181).ToUnixTimeMilliseconds();
        var oldEnd = DateTimeOffset.UtcNow.AddDays(-180).ToUnixTimeMilliseconds();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Affiliate.GetInviteesAsync(new OkxAffiliateInviteeListRequest
        {
            PeriodType = OkxAffiliatePeriodType.Custom,
            Begin = oldStart,
            End = oldEnd,
        }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Affiliate.GetInviteesAsync(new OkxAffiliateInviteeListRequest
        {
            JoinTimeBegin = oldStart,
            JoinTimeEnd = oldEnd,
        }));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task InviteeEndpoints_EnforceSeparateThreePerSecondUserLimits()
    {
        using var listServer = CreateServer("/api/v5/affiliate/invitee/list", "invitee-list.json");
        var listClient = CreateClient(listServer, RateLimitingBehavior.Fail);

        for (var i = 0; i < 3; i++)
            Assert.True((await listClient.Affiliate.GetInviteesAsync()).Success);
        Assert.False((await listClient.Affiliate.GetInviteesAsync()).Success);

        using var detailServer = CreateServer("/api/v5/affiliate/invitee/detail", "invitee-detail.json");
        var detailClient = CreateClient(detailServer, RateLimitingBehavior.Fail);

        for (var i = 0; i < 3; i++)
            Assert.True((await detailClient.Affiliate.GetInviteeAsync(835449167911924693)).Success);
        Assert.False((await detailClient.Affiliate.GetInviteeAsync(835449167911924693)).Success);

        Assert.Equal(3, listServer.Requests.Count);
        Assert.Equal(3, detailServer.Requests.Count);
    }

    [Theory]
    [InlineData(0, 100)]
    [InlineData(1, 0)]
    [InlineData(1, 101)]
    public async Task PaginatedEndpoints_RejectInvalidPageSettings(int page, int limit)
    {
        using var server = CreateServer("/api/v5/affiliate/link/list", "link-list.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Affiliate.GetAffiliateLinksAsync(new OkxAffiliateLinkListRequest
        {
            Page = page,
            Limit = limit,
        }));
        Assert.Empty(server.Requests);
    }

    private static string GetDecodedQuery(LocalOkxRestServer server)
        => Uri.UnescapeDataString(Assert.Single(server.Requests).Query);

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Affiliate", fixtureName),
        });

    private static OkxRestApiClient CreateClient(
        LocalOkxRestServer server,
        RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = rateLimitingBehavior,
        };

        return new OkxRestApiClient(options);
    }
}
