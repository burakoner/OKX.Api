namespace OKX.Api.Affiliate;

/// <summary>
/// OKX Rest Api Affiliate Client
/// </summary>
public class OkxAffiliateRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    private const long NinetyDaysInMilliseconds = 90L * 24L * 60L * 60L * 1000L;
    private const long OneHundredEightyDaysInMilliseconds = 180L * 24L * 60L * 60L * 1000L;

    /// <summary>
    /// Get aggregated affiliate performance metrics for a statistics window.
    /// </summary>
    /// <param name="request">Performance query. When omitted, OKX returns lifetime statistics.</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxAffiliatePerformanceSummary>> GetPerformanceSummaryAsync(
        OkxAffiliatePerformanceSummaryRequest? request = null,
        CancellationToken ct = default)
    {
        request ??= new OkxAffiliatePerformanceSummaryRequest();
        ValidatePeriod(request.PeriodType, request.Begin, request.End, maximumRange: null, nameof(request));

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("periodType", request.PeriodType);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());

        return ProcessOneRequestAsync<OkxAffiliatePerformanceSummary>(GetUri("api/v5/affiliate/performance/summary"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the invitee's detail
    /// </summary>
    /// <param name="uid">UID of the invitee. Only applicable to the UID of invitee master account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAffiliateInvitee>> GetInviteeAsync(long uid, CancellationToken ct = default)
        => GetInviteeAsync(new OkxAffiliateInviteeDetailRequest
        {
            UserId = uid.ToOkxString(),
        }, ct);

    /// <summary>
    /// Get an invitee's current details and optional period trading volume.
    /// </summary>
    /// <param name="request">Invitee detail query</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxAffiliateInvitee>> GetInviteeAsync(OkxAffiliateInviteeDetailRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.UserId))
            throw new ArgumentException("Invitee UID is required.", nameof(request));
        ValidateOptionalEnum(request.PeriodType, nameof(request.PeriodType));
        if (request.PeriodType == OkxAffiliatePeriodType.Custom)
            throw new ArgumentException("Custom statistics windows are not supported by the invitee detail endpoint.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "uid", request.UserId },
        };
        parameters.AddOptionalEnum("periodType", request.PeriodType);

        return ProcessOneRequestAsync<OkxAffiliateInvitee>(GetUri("api/v5/affiliate/invitee/detail"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a paginated list of affiliate invitees with current filters and statistics.
    /// </summary>
    /// <param name="request">Invitee list query</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxPaginatedResult<OkxAffiliateInviteeListItem>>> GetInviteesAsync(
        OkxAffiliateInviteeListRequest? request = null,
        CancellationToken ct = default)
    {
        request ??= new OkxAffiliateInviteeListRequest();
        ValidatePagination(request.Page, request.Limit, nameof(request));
        ValidatePeriod(request.PeriodType, request.Begin, request.End, NinetyDaysInMilliseconds, nameof(request));
        ValidateOptionalEnum(request.CommissionCategory, nameof(request.CommissionCategory));
        ValidateOptionalEnum(request.OrderBy, nameof(request.OrderBy));
        ValidateOptionalEnum(request.OrderDirection, nameof(request.OrderDirection));
        ValidateOptionalEnum(request.KycStatus, nameof(request.KycStatus));
        ValidateInclusiveRangePair(request.JoinTimeBegin, request.JoinTimeEnd, NinetyDaysInMilliseconds, "joinTimeBegin and joinTimeEnd", nameof(request));
        if (request.PeriodType == OkxAffiliatePeriodType.Custom)
            ValidateRecentStart(request.Begin, "begin", nameof(request));
        ValidateRecentStart(request.JoinTimeBegin, "joinTimeBegin", nameof(request));

        List<string>? userIds = null;
        if (request.UserIds is not null)
        {
            userIds = request.UserIds.ToList();
            if (userIds.Count > 100)
                throw new ArgumentOutOfRangeException(nameof(request), userIds.Count, "At most 100 invitee UIDs can be requested.");
            if (userIds.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Invitee UIDs cannot contain empty values.", nameof(request));
        }

        var parameters = CreatePaginationParameters(request.Page, request.Limit);
        parameters.AddOptionalEnum("periodType", request.PeriodType);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("keyword", request.Keyword);
        parameters.AddOptionalEnum("commissionCategory", request.CommissionCategory);
        parameters.AddOptionalEnum("orderBy", request.OrderBy);
        parameters.AddOptionalEnum("orderDir", request.OrderDirection);
        parameters.AddOptionalEnum("kycStatus", request.KycStatus);
        parameters.AddOptional("subAffiliateUid", request.SubAffiliateUserId);
        if (userIds?.Count > 0)
            parameters.Add("uid", string.Join(",", userIds));
        parameters.AddOptional("joinTimeBegin", request.JoinTimeBegin?.ToOkxString());
        parameters.AddOptional("joinTimeEnd", request.JoinTimeEnd?.ToOkxString());

        return ProcessPaginatedListRequestAsync<OkxAffiliateInviteeListItem>(GetUri("api/v5/affiliate/invitee/list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the authenticated affiliate's paginated invitation link list.
    /// </summary>
    /// <param name="request">Link list query</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxPaginatedResult<OkxAffiliateLink>>> GetAffiliateLinksAsync(
        OkxAffiliateLinkListRequest? request = null,
        CancellationToken ct = default)
    {
        request ??= new OkxAffiliateLinkListRequest();
        ValidatePagination(request.Page, request.Limit, nameof(request));
        ValidateOptionalEnum(request.LinkType, nameof(request.LinkType));
        ValidateOptionalEnum(request.LinkStatus, nameof(request.LinkStatus));

        var parameters = CreatePaginationParameters(request.Page, request.Limit);
        parameters.AddOptionalEnum("linkType", request.LinkType);
        parameters.AddOptionalEnum("linkStatus", request.LinkStatus);

        return ProcessPaginatedListRequestAsync<OkxAffiliateLink>(GetUri("api/v5/affiliate/link/list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get links on which the authenticated user is the co-inviter.
    /// </summary>
    /// <param name="request">Co-inviter link list query</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxPaginatedResult<OkxAffiliateCoInviterLink>>> GetCoInviterLinksAsync(
        OkxAffiliateCoInviterLinkListRequest? request = null,
        CancellationToken ct = default)
    {
        request ??= new OkxAffiliateCoInviterLinkListRequest();
        ValidatePagination(request.Page, request.Limit, nameof(request));
        ValidateOptionalEnum(request.LinkStatus, nameof(request.LinkStatus));

        var parameters = CreatePaginationParameters(request.Page, request.Limit);
        parameters.AddOptionalEnum("linkStatus", request.LinkStatus);

        return ProcessPaginatedListRequestAsync<OkxAffiliateCoInviterLink>(GetUri("api/v5/affiliate/co-inviter/list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get paginated direct and indirect sub-affiliates with lifetime performance metrics.
    /// </summary>
    /// <param name="request">Sub-affiliate list query</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<OkxPaginatedResult<OkxAffiliateSubAffiliate>>> GetSubAffiliatesAsync(
        OkxAffiliateSubAffiliateListRequest? request = null,
        CancellationToken ct = default)
    {
        request ??= new OkxAffiliateSubAffiliateListRequest();
        ValidatePagination(request.Page, request.Limit, nameof(request));
        ValidateOptionalEnum(request.CommissionCategory, nameof(request.CommissionCategory));
        ValidateOptionalEnum(request.OrderBy, nameof(request.OrderBy));
        ValidateOptionalEnum(request.OrderDirection, nameof(request.OrderDirection));

        var parameters = CreatePaginationParameters(request.Page, request.Limit);
        parameters.AddOptional("keyword", request.Keyword);
        parameters.AddOptionalEnum("commissionCategory", request.CommissionCategory);
        parameters.AddOptionalEnum("orderBy", request.OrderBy);
        parameters.AddOptionalEnum("orderDir", request.OrderDirection);

        return ProcessPaginatedListRequestAsync<OkxAffiliateSubAffiliate>(GetUri("api/v5/affiliate/sub-affiliate/list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// This endpoint will be offline soon, please use Get the invitee's detail.
    /// It is used to get the user's affiliate rebate information for affiliate.
    /// </summary>
    /// <param name="apiKey">The user's API key. Only applicable to the API key of invitee master account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAffiliateRebateInformation>> GetRebateInformationAsync(string apiKey, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "apiKey", apiKey },
        };

        return ProcessOneRequestAsync<OkxAffiliateRebateInformation>(GetUri("api/v5/users/partner/if-rebate"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    private static ParameterCollection CreatePaginationParameters(int page, int limit)
        => new()
        {
            { "page", page.ToOkxString() },
            { "limit", limit.ToOkxString() },
        };

    private static void ValidatePagination(int page, int limit, string parameterName)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(parameterName, page, "Page must be at least 1.");
        if (limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(parameterName, limit, "Page size must be between 1 and 100.");
    }

    private static void ValidatePeriod(
        OkxAffiliatePeriodType? periodType,
        long? begin,
        long? end,
        long? maximumRange,
        string parameterName)
    {
        ValidateOptionalEnum(periodType, nameof(periodType));
        if (periodType != OkxAffiliatePeriodType.Custom)
            return;

        ValidateInclusiveRangePair(begin, end, maximumRange, "begin and end", parameterName);
    }

    private static void ValidateInclusiveRangePair(
        long? begin,
        long? end,
        long? maximumRange,
        string fieldNames,
        string parameterName)
    {
        if (begin.HasValue != end.HasValue)
            throw new ArgumentException($"{fieldNames} must be supplied together.", parameterName);
        if (!begin.HasValue)
            return;
        if (begin.Value > end!.Value)
            throw new ArgumentException($"The start of {fieldNames} cannot be after its end.", parameterName);
        var range = (decimal)end.Value - begin.Value;
        if (maximumRange.HasValue && range > maximumRange.Value)
            throw new ArgumentOutOfRangeException(parameterName, range, $"The {fieldNames} range cannot exceed 90 days.");
    }

    private static void ValidateOptionalEnum<T>(T? value, string parameterName) where T : struct, Enum
    {
        if (value.HasValue && !Enum.IsDefined(typeof(T), value.Value))
            throw new ArgumentOutOfRangeException(parameterName, value.Value, $"Unsupported {typeof(T).Name} value.");
    }

    private static void ValidateRecentStart(long? timestamp, string fieldName, string parameterName)
    {
        if (!timestamp.HasValue)
            return;

        var earliestAllowed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - OneHundredEightyDaysInMilliseconds;
        if (timestamp.Value < earliestAllowed)
            throw new ArgumentOutOfRangeException(parameterName, timestamp.Value, $"{fieldName} cannot be earlier than 180 days ago.");
    }
}
