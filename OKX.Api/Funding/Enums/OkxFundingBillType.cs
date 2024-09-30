namespace OKX.Api.Funding;

/// <summary>
/// OKX Funding Bill Type
/// </summary>
public enum OkxFundingBillType
{
    /// <summary>
    /// Deposit
    /// </summary>
    [Map("1")]
    Deposit = 1,

    /// <summary>
    /// Withdrawal
    /// </summary>
    [Map("2")]
    Withdrawal = 2,

    /// <summary>
    /// CanceledWithdrawal
    /// </summary>
    [Map("13")]
    CanceledWithdrawal = 13,

    /// <summary>
    /// TransferToFutures
    /// </summary>
    [Map("18")]
    TransferToFutures = 18,

    /// <summary>
    /// TransferFromFutures
    /// </summary>
    [Map("19")]
    TransferFromFutures = 19,

    /// <summary>
    /// TransferToSubAccount
    /// </summary>
    [Map("20")]
    TransferToSubAccount = 20,

    /// <summary>
    /// TransferFromSubAccount
    /// </summary>
    [Map("21")]
    TransferFromSubAccount = 21,

    /// <summary>
    /// TransferOutFromSubToMasterAccount
    /// </summary>
    [Map("22")]
    TransferOutFromSubToMasterAccount = 22,

    /// <summary>
    /// TransferInFromMasterToSubAccount
    /// </summary>
    [Map("23")]
    TransferInFromMasterToSubAccount = 23,

    /// <summary>
    /// ManuallyClaimedAirdrop
    /// </summary>
    [Map("28")]
    ManuallyClaimedAirdrop = 28,

    /// <summary>
    /// TransferToMargin
    /// </summary>
    [Map("33")]
    TransferToMargin = 33,

    /// <summary>
    /// TransferFromMargin
    /// </summary>
    [Map("34")]
    TransferFromMargin = 34,

    /// <summary>
    /// TransferToSpot
    /// </summary>
    [Map("37")]
    TransferToSpot = 37,

    /// <summary>
    /// TransferFromSpot
    /// </summary>
    [Map("38")]
    TransferFromSpot = 38,

    /// <summary>
    /// TradingFeesSettledByLoyaltyPoints
    /// </summary>
    [Map("41")]
    TradingFeesSettledByLoyaltyPoints = 41,

    /// <summary>
    /// LoyaltyPointsPurchase
    /// </summary>
    [Map("42")]
    LoyaltyPointsPurchase = 42,

    /// <summary>
    /// SystemReversal
    /// </summary>
    [Map("47")]
    SystemReversal = 47,

    /// <summary>
    /// EventReward
    /// </summary>
    [Map("48")]
    EventReward = 48,

    /// <summary>
    /// EventGiveaway
    /// </summary>
    [Map("49")]
    EventGiveaway = 49,

    /// <summary>
    /// ReceivedFromAppointments
    /// </summary>
    [Map("50")]
    ReceivedFromAppointments = 50,

    /// <summary>
    /// DeductedFromAppointments
    /// </summary>
    [Map("51")]
    DeductedFromAppointments = 51,

    /// <summary>
    /// RedPacketSent
    /// </summary>
    [Map("52")]
    RedPacketSent = 52,

    /// <summary>
    /// RedPacketSnatched
    /// </summary>
    [Map("53")]
    RedPacketSnatched = 53,

    /// <summary>
    /// RedPacketRefunded
    /// </summary>
    [Map("54")]
    RedPacketRefunded = 54,

    /// <summary>
    /// TransferToPerpetual
    /// </summary>
    [Map("55")]
    TransferToPerpetual = 55,

    /// <summary>
    /// TransferFromPerpetual
    /// </summary>
    [Map("56")]
    TransferFromPerpetual = 56,

    /// <summary>
    /// TransferFromHedgingAccount
    /// </summary>
    [Map("59")]
    TransferFromHedgingAccount = 59,

    /// <summary>
    /// TransferToHedgingAccount
    /// </summary>
    [Map("60")]
    TransferToHedgingAccount = 60,

    /// <summary>
    /// Conversion
    /// </summary>
    [Map("61")]
    Conversion = 61,

    /// <summary>
    /// TransferFromOptions
    /// </summary>
    [Map("62")]
    TransferFromOptions = 62,

    /// <summary>
    /// TransferToOptions
    /// </summary>
    [Map("63")]
    TransferToOptions = 63,

    /// <summary>
    /// ClaimRebateCard
    /// </summary>
    [Map("68")]
    ClaimRebateCard = 68,

    /// <summary>
    /// DistributeRebateCard
    /// </summary>
    [Map("69")]
    DistributeRebateCard = 69,

    /// <summary>
    /// TokenReceived
    /// </summary>
    [Map("72")]
    TokenReceived = 72,

    /// <summary>
    /// TokenGivenAway
    /// </summary>
    [Map("73")]
    TokenGivenAway = 73,

    /// <summary>
    /// TokenRefunded
    /// </summary>
    [Map("74")]
    TokenRefunded = 74,

    /// <summary>
    /// SubscriptionToSavings
    /// </summary>
    [Map("75")]
    SubscriptionToSavings = 75,

    /// <summary>
    /// RedemptionToSavings
    /// </summary>
    [Map("76")]
    RedemptionToSavings = 76,

    /// <summary>
    /// Distribute
    /// </summary>
    [Map("77")]
    Distribute = 77,

    /// <summary>
    /// LockUp
    /// </summary>
    [Map("78")]
    LockUp = 78,

    /// <summary>
    /// NodeVoting
    /// </summary>
    [Map("79")]
    NodeVoting = 79,

    /// <summary>
    /// Staking80
    /// </summary>
    [Map("80")]
    Staking80 = 80,

    /// <summary>
    /// VoteRedemption
    /// </summary>
    [Map("81")]
    VoteRedemption = 81,

    /// <summary>
    /// StakingRedemption82
    /// </summary>
    [Map("82")]
    StakingRedemption82 = 82,

    /// <summary>
    /// StakingYield
    /// </summary>
    [Map("83")]
    StakingYield = 83,

    /// <summary>
    /// ViolationFee
    /// </summary>
    [Map("84")]
    ViolationFee = 84,

    /// <summary>
    /// PowMiningYield
    /// </summary>
    [Map("85")]
    PowMiningYield = 85,

    /// <summary>
    /// CloudMiningPay
    /// </summary>
    [Map("86")]
    CloudMiningPay = 86,

    /// <summary>
    /// CloudMiningYield
    /// </summary>
    [Map("87")]
    CloudMiningYield = 87,

    /// <summary>
    /// Subsidy
    /// </summary>
    [Map("88")]
    Subsidy = 88,

    /// <summary>
    /// Staking89
    /// </summary>
    [Map("89")]
    Staking89 = 89,

    /// <summary>
    /// StakingSubscription
    /// </summary>
    [Map("90")]
    StakingSubscription = 90,

    /// <summary>
    /// StakingRedemption91
    /// </summary>
    [Map("91")]
    StakingRedemption91 = 91,

    /// <summary>
    /// AddCollateral
    /// </summary>
    [Map("92")]
    AddCollateral = 92,

    /// <summary>
    /// RedeemCollateral
    /// </summary>
    [Map("93")]
    RedeemCollateral = 93,

    /// <summary>
    /// Investment
    /// </summary>
    [Map("94")]
    Investment = 94,

    /// <summary>
    /// BorrowerBorrows
    /// </summary>
    [Map("95")]
    BorrowerBorrows = 95,

    /// <summary>
    /// PrincipalTransferredIn
    /// </summary>
    [Map("96")]
    PrincipalTransferredIn = 96,

    /// <summary>
    /// BorrowerTransferredLoanOut
    /// </summary>
    [Map("97")]
    BorrowerTransferredLoanOut = 97,

    /// <summary>
    /// BorrowerTransferredInterestOut
    /// </summary>
    [Map("98")]
    BorrowerTransferredInterestOut = 98,

    /// <summary>
    /// InvestorTransferredInterestIn
    /// </summary>
    [Map("99")]
    InvestorTransferredInterestIn = 99,

    /// <summary>
    /// PrepaymentPenaltyTransferredIn
    /// </summary>
    [Map("102")]
    PrepaymentPenaltyTransferredIn = 102,

    /// <summary>
    /// PrepaymentPenaltyTransferredOut
    /// </summary>
    [Map("103")]
    PrepaymentPenaltyTransferredOut = 103,

    /// <summary>
    /// MortgageFeeTransferredIn
    /// </summary>
    [Map("104")]
    MortgageFeeTransferredIn = 104,

    /// <summary>
    /// MortgageFeeTransferredOut
    /// </summary>
    [Map("105")]
    MortgageFeeTransferredOut = 105,

    /// <summary>
    /// OverdueFeeTransferredIn
    /// </summary>
    [Map("106")]
    OverdueFeeTransferredIn = 106,

    /// <summary>
    /// OverdueFeeTransferredOut
    /// </summary>
    [Map("107")]
    OverdueFeeTransferredOut = 107,

    /// <summary>
    /// OverdueInterestTransferredOut
    /// </summary>
    [Map("108")]
    OverdueInterestTransferredOut = 108,

    /// <summary>
    /// OverdueInterestTransferredIn
    /// </summary>
    [Map("109")]
    OverdueInterestTransferredIn = 109,

    /// <summary>
    /// CollateralForClosedPositionTransferredIn
    /// </summary>
    [Map("110")]
    CollateralForClosedPositionTransferredIn = 110,

    /// <summary>
    /// CollateralForClosedPositionTransferredOut
    /// </summary>
    [Map("111")]
    CollateralForClosedPositionTransferredOut = 111,

    /// <summary>
    /// CollateralForLiquidationTransferredIn
    /// </summary>
    [Map("112")]
    CollateralForLiquidationTransferredIn = 112,

    /// <summary>
    /// CollateralForLiquidationTransferredOut
    /// </summary>
    [Map("113")]
    CollateralForLiquidationTransferredOut = 113,

    /// <summary>
    /// InsuranceFundTransferredIn
    /// </summary>
    [Map("114")]
    InsuranceFundTransferredIn = 114,

    /// <summary>
    /// InsuranceFundTransferredOut
    /// </summary>
    [Map("115")]
    InsuranceFundTransferredOut = 115,

    /// <summary>
    /// PlaceAnOrder
    /// </summary>
    [Map("116")]
    PlaceAnOrder = 116,

    /// <summary>
    /// FulfillAnOrder
    /// </summary>
    [Map("117")]
    FulfillAnOrder = 117,

    /// <summary>
    /// CancelAnOrder
    /// </summary>
    [Map("118")]
    CancelAnOrder = 118,

    /// <summary>
    /// MerchantsUnlockDeposit
    /// </summary>
    [Map("119")]
    MerchantsUnlockDeposit = 119,

    /// <summary>
    /// MerchantsAddDeposit
    /// </summary>
    [Map("120")]
    MerchantsAddDeposit = 120,

    /// <summary>
    /// FiatgatewayPlaceAnOrder
    /// </summary>
    [Map("121")]
    FiatgatewayPlaceAnOrder = 121,

    /// <summary>
    /// FiatgatewayCancelAnOrder
    /// </summary>
    [Map("122")]
    FiatgatewayCancelAnOrder = 122,

    /// <summary>
    /// FiatgatewayFulfillAnOrder
    /// </summary>
    [Map("123")]
    FiatgatewayFulfillAnOrder = 123,

    /// <summary>
    /// JumpstartUnlocking
    /// </summary>
    [Map("124")]
    JumpstartUnlocking = 124,

    /// <summary>
    /// ManualDeposit
    /// </summary>
    [Map("125")]
    ManualDeposit = 125,

    /// <summary>
    /// InterestDeposit
    /// </summary>
    [Map("126")]
    InterestDeposit = 126,

    /// <summary>
    /// InvestmentFeeTransferredIn
    /// </summary>
    [Map("127")]
    InvestmentFeeTransferredIn = 127,

    /// <summary>
    /// InvestmentFeeTransferredOut
    /// </summary>
    [Map("128")]
    InvestmentFeeTransferredOut = 128,

    /// <summary>
    /// RewardsTransferredIn
    /// </summary>
    [Map("129")]
    RewardsTransferredIn = 129,

    /// <summary>
    /// TransferredFromUnifiedAccount
    /// </summary>
    [Map("130")]
    TransferredFromUnifiedAccount = 130,

    /// <summary>
    /// TransferredToUnifiedAccount
    /// </summary>
    [Map("131")]
    TransferredToUnifiedAccount = 131,

    /// <summary>
    /// FrozenByCustomerService
    /// </summary>
    [Map("132")]
    FrozenByCustomerService = 132,

    /// <summary>
    /// UnfrozenByCustomerService
    /// </summary>
    [Map("133")]
    UnfrozenByCustomerService = 133,

    /// <summary>
    /// TransferredByCustomerService
    /// </summary>
    [Map("134")]
    TransferredByCustomerService = 134,

    /// <summary>
    /// CrossChainExchange
    /// </summary>
    [Map("135")]
    CrossChainExchange = 135,

    /// <summary>
    /// Convert
    /// </summary>
    [Map("136")]
    Convert = 136,

    /// <summary>
    /// Eth20Subscription
    /// </summary>
    [Map("137")]
    Eth20Subscription = 137,

    /// <summary>
    /// Eth20Swapping
    /// </summary>
    [Map("138")]
    Eth20Swapping = 138,

    /// <summary>
    /// Eth20Earnings
    /// </summary>
    [Map("139")]
    Eth20Earnings = 139,

    /// <summary>
    /// SystemReverse
    /// </summary>
    [Map("143")]
    SystemReverse = 143,

    /// <summary>
    /// TransferOutOfUnifiedAccountReserve
    /// </summary>
    [Map("144")]
    TransferOutOfUnifiedAccountReserve = 144,

    /// <summary>
    /// RewardExpired
    /// </summary>
    [Map("145")]
    RewardExpired = 145,

    /// <summary>
    /// CustomerFeedback
    /// </summary>
    [Map("146")]
    CustomerFeedback = 146,

    /// <summary>
    /// VestedSushiRewards
    /// </summary>
    [Map("147")]
    VestedSushiRewards = 147,

    /// <summary>
    /// AffiliateCommission
    /// </summary>
    [Map("150")]
    AffiliateCommission = 150,

    /// <summary>
    /// ReferralReward
    /// </summary>
    [Map("151")]
    ReferralReward = 151,

    /// <summary>
    /// BrokerReward
    /// </summary>
    [Map("152")]
    BrokerReward = 152,

    /// <summary>
    /// JoinerReward
    /// </summary>
    [Map("153")]
    JoinerReward = 153,

    /// <summary>
    /// MysteryBoxReward
    /// </summary>
    [Map("154")]
    MysteryBoxReward = 154,

    /// <summary>
    /// RewardsWithdraw
    /// </summary>
    [Map("155")]
    RewardsWithdraw = 155,

    /// <summary>
    /// FeeRebate156
    /// </summary>
    [Map("156")]
    FeeRebate156 = 156,

    /// <summary>
    /// CollectedCrypto
    /// </summary>
    [Map("157")]
    CollectedCrypto = 157,

    /// <summary>
    /// DualInvestmentSubscribe
    /// </summary>
    [Map("160")]
    DualInvestmentSubscribe = 160,

    /// <summary>
    /// DualInvestmentCollection
    /// </summary>
    [Map("161")]
    DualInvestmentCollection = 161,

    /// <summary>
    /// DualInvestmentProfit
    /// </summary>
    [Map("162")]
    DualInvestmentProfit = 162,

    /// <summary>
    /// DualInvestmentRefund
    /// </summary>
    [Map("163")]
    DualInvestmentRefund = 163,

    /// <summary>
    /// NewYearRewards2022
    /// </summary>
    [Map("169")]
    NewYearRewards2022 = 169,

    /// <summary>
    /// SubAffiliateCommission
    /// </summary>
    [Map("172")]
    SubAffiliateCommission = 172,

    /// <summary>
    /// FeeRebate173
    /// </summary>
    [Map("173")]
    FeeRebate173 = 173,

    /// <summary>
    /// Pay
    /// </summary>
    [Map("174")]
    Pay = 174,

    /// <summary>
    /// LockedCollateral
    /// </summary>
    [Map("175")]
    LockedCollateral = 175,

    /// <summary>
    /// Loan
    /// </summary>
    [Map("176")]
    Loan = 176,

    /// <summary>
    /// AddedCollateral
    /// </summary>
    [Map("177")]
    AddedCollateral = 177,

    /// <summary>
    /// ReturnedCollateral
    /// </summary>
    [Map("178")]
    ReturnedCollateral = 178,

    /// <summary>
    /// Repayment
    /// </summary>
    [Map("179")]
    Repayment = 179,

    /// <summary>
    /// UnlockedCollateral
    /// </summary>
    [Map("180")]
    UnlockedCollateral = 180,

    /// <summary>
    /// AirdropPayment
    /// </summary>
    [Map("181")]
    AirdropPayment = 181,

    /// <summary>
    /// FeedbackBounty
    /// </summary>
    [Map("182")]
    FeedbackBounty = 182,

    /// <summary>
    /// InviteFriendsRewards
    /// </summary>
    [Map("183")]
    InviteFriendsRewards = 183,

    /// <summary>
    /// DivideTheRewardPool
    /// </summary>
    [Map("184")]
    DivideTheRewardPool = 184,

    /// <summary>
    /// BrokerConvertReward
    /// </summary>
    [Map("185")]
    BrokerConvertReward = 185,

    /// <summary>
    /// Freeeth
    /// </summary>
    [Map("186")]
    Freeeth = 186,

    /// <summary>
    /// ConvertTransfer
    /// </summary>
    [Map("187")]
    ConvertTransfer = 187,

    /// <summary>
    /// SlotAuctionConversion
    /// </summary>
    [Map("188")]
    SlotAuctionConversion = 188,

    /// <summary>
    /// MysteryBoxBonus
    /// </summary>
    [Map("189")]
    MysteryBoxBonus = 189,

    /// <summary>
    /// CardPaymentBuy
    /// </summary>
    [Map("193")]
    CardPaymentBuy = 193,

    /// <summary>
    /// UntradableAssetWithdrawal
    /// </summary>
    [Map("195")]
    UntradableAssetWithdrawal = 195,

    /// <summary>
    /// UntradableAssetWithdrawalRevoked
    /// </summary>
    [Map("196")]
    UntradableAssetWithdrawalRevoked = 196,

    /// <summary>
    /// UntradableAssetDeposit
    /// </summary>
    [Map("197")]
    UntradableAssetDeposit = 197,

    /// <summary>
    /// UntradableAssetCollectionReduce
    /// </summary>
    [Map("198")]
    UntradableAssetCollectionReduce = 198,

    /// <summary>
    /// UntradableAssetCollectionIncrease
    /// </summary>
    [Map("199")]
    UntradableAssetCollectionIncrease = 199,

    /// <summary>
    /// Buy
    /// </summary>
    [Map("200")]
    Buy = 200,

    /// <summary>
    /// PriceLockSubscribe
    /// </summary>
    [Map("202")]
    PriceLockSubscribe = 202,

    /// <summary>
    /// PriceLockCollection
    /// </summary>
    [Map("203")]
    PriceLockCollection = 203,

    /// <summary>
    /// PriceLockProfit
    /// </summary>
    [Map("204")]
    PriceLockProfit = 204,

    /// <summary>
    /// PriceLockRefund
    /// </summary>
    [Map("205")]
    PriceLockRefund = 205,

    /// <summary>
    /// DualInvestmentLiteSubscribe
    /// </summary>
    [Map("207")]
    DualInvestmentLiteSubscribe = 207,

    /// <summary>
    /// DualInvestmentLiteCollection
    /// </summary>
    [Map("208")]
    DualInvestmentLiteCollection = 208,

    /// <summary>
    /// DualInvestmentLiteProfit
    /// </summary>
    [Map("209")]
    DualInvestmentLiteProfit = 209,

    /// <summary>
    /// DualInvestmentLiteRefund
    /// </summary>
    [Map("210")]
    DualInvestmentLiteRefund = 210,

    /// <summary>
    /// WinCryptoWithSatoshi
    /// </summary>
    [Map("211")]
    WinCryptoWithSatoshi = 211,

    /// <summary>
    /// MultiCollateralLoanCollateralLocked
    /// </summary>
    [Map("212")]
    MultiCollateralLoanCollateralLocked = 212,

    /// <summary>
    /// CollateralTransferedOutFromUserAccount
    /// </summary>
    [Map("213")]
    CollateralTransferedOutFromUserAccount = 213,

    /// <summary>
    /// CollateralReturnedToUsers
    /// </summary>
    [Map("214")]
    CollateralReturnedToUsers = 214,

    /// <summary>
    /// MultiCollateralLoanCollateralReleased
    /// </summary>
    [Map("215")]
    MultiCollateralLoanCollateralReleased = 215,

    /// <summary>
    /// LoanTransferredToUserAccount
    /// </summary>
    [Map("216")]
    LoanTransferredToUserAccount = 216,

    /// <summary>
    /// MultiCollateralLoanBorrowed
    /// </summary>
    [Map("217")]
    MultiCollateralLoanBorrowed = 217,

    /// <summary>
    /// MultiCollateralLoanRepaid
    /// </summary>
    [Map("218")]
    MultiCollateralLoanRepaid = 218,

    /// <summary>
    /// RepaymentTransferredFromUserAccount
    /// </summary>
    [Map("219")]
    RepaymentTransferredFromUserAccount = 219,

    /// <summary>
    /// DelistedCrypto
    /// </summary>
    [Map("220")]
    DelistedCrypto = 220,

    /// <summary>
    /// BlockchainWithdrawalFee
    /// </summary>
    [Map("221")]
    BlockchainWithdrawalFee = 221,

    /// <summary>
    /// WithdrawalFeeRefund
    /// </summary>
    [Map("222")]
    WithdrawalFeeRefund = 222,

    /// <summary>
    /// ProfitShare
    /// </summary>
    [Map("223")]
    ProfitShare = 223,

    /// <summary>
    /// ServiceFee
    /// </summary>
    [Map("224")]
    ServiceFee = 224,

    /// <summary>
    /// SharkFinSubscribe
    /// </summary>
    [Map("225")]
    SharkFinSubscribe = 225,

    /// <summary>
    /// SharkFinCollection
    /// </summary>
    [Map("226")]
    SharkFinCollection = 226,

    /// <summary>
    /// SharkFinProfit
    /// </summary>
    [Map("227")]
    SharkFinProfit = 227,

    /// <summary>
    /// SharkFinRefund
    /// </summary>
    [Map("228")]
    SharkFinRefund = 228,

    /// <summary>
    /// Airdrop
    /// </summary>
    [Map("229")]
    Airdrop = 229,

    /// <summary>
    /// TokenMigrationCompleted
    /// </summary>
    [Map("230")]
    TokenMigrationCompleted = 230,

    /// <summary>
    /// SubsidizedInterestReceived
    /// </summary>
    [Map("232")]
    SubsidizedInterestReceived = 232,

    /// <summary>
    /// BrokerRebateCompensation
    /// </summary>
    [Map("233")]
    BrokerRebateCompensation = 233,

    /// <summary>
    /// TransferOutTradingSubAccount
    /// </summary>
    [Map("284")]
    TransferOutTradingSubAccount = 284,

    /// <summary>
    /// TransferInTradingSubAccount
    /// </summary>
    [Map("285")]
    TransferInTradingSubAccount = 285,

    /// <summary>
    /// TransferOutCustodyFundingAccount
    /// </summary>
    [Map("286")]
    TransferOutCustodyFundingAccount = 286,

    /// <summary>
    /// TransferInCustodyFundingAccount
    /// </summary>
    [Map("287")]
    TransferInCustodyFundingAccount = 287,

    /// <summary>
    /// CustodyFundDelegation
    /// </summary>
    [Map("288")]
    CustodyFundDelegation = 288,

    /// <summary>
    /// CustodyFundUndelegation
    /// </summary>
    [Map("289")]
    CustodyFundUndelegation = 289,
}