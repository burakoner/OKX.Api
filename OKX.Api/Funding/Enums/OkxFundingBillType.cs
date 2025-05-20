namespace OKX.Api.Funding;

/// <summary>
/// OKX Funding Bill Type
/// </summary>
public enum OkxFundingBillType:int
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
    [Obsolete]
    [Map("18")]
    TransferToFutures = 18,

    /// <summary>
    /// TransferFromFutures
    /// </summary>
    [Obsolete]
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
    [Obsolete]
    [Map("33")]
    TransferToMargin = 33,

    /// <summary>
    /// TransferFromMargin
    /// </summary>
    [Obsolete]
    [Map("34")]
    TransferFromMargin = 34,

    /// <summary>
    /// TransferToSpot
    /// </summary>
    [Obsolete]
    [Map("37")]
    TransferToSpot = 37,

    /// <summary>
    /// TransferFromSpot
    /// </summary>
    [Obsolete]
    [Map("38")]
    TransferFromSpot = 38,

    /// <summary>
    /// TradingFeesSettledByLoyaltyPoints
    /// </summary>
    [Obsolete]
    [Map("41")]
    TradingFeesSettledByLoyaltyPoints = 41,

    /// <summary>
    /// LoyaltyPointsPurchase
    /// </summary>
    [Obsolete]
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
    [Obsolete]
    [Map("50")]
    ReceivedFromAppointments = 50,

    /// <summary>
    /// DeductedFromAppointments
    /// </summary>
    [Obsolete]
    [Map("51")]
    DeductedFromAppointments = 51,

    /// <summary>
    /// RedPacketSent
    /// </summary>
    [Obsolete]
    [Map("52")]
    RedPacketSent = 52,

    /// <summary>
    /// RedPacketSnatched
    /// </summary>
    [Obsolete]
    [Map("53")]
    RedPacketSnatched = 53,

    /// <summary>
    /// RedPacketRefunded
    /// </summary>
    [Obsolete]
    [Map("54")]
    RedPacketRefunded = 54,

    /// <summary>
    /// TransferToPerpetual
    /// </summary>
    [Obsolete]
    [Map("55")]
    TransferToPerpetual = 55,

    /// <summary>
    /// TransferFromPerpetual
    /// </summary>
    [Obsolete]
    [Map("56")]
    TransferFromPerpetual = 56,

    /// <summary>
    /// TransferFromHedgingAccount
    /// </summary>
    [Obsolete]
    [Map("59")]
    TransferFromHedgingAccount = 59,

    /// <summary>
    /// TransferToHedgingAccount
    /// </summary>
    [Obsolete]
    [Map("60")]
    TransferToHedgingAccount = 60,

    /// <summary>
    /// Conversion
    /// </summary>
    [Obsolete]
    [Map("61")]
    Conversion = 61,

    /// <summary>
    /// TransferFromOptions
    /// </summary>
    [Obsolete]
    [Map("62")]
    TransferFromOptions = 62,

    /// <summary>
    /// TransferToOptions
    /// </summary>
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
    [Map("79")]
    NodeVoting = 79,

    /// <summary>
    /// DEFI/Staking subscription
    /// </summary>
    [Map("80")]
    DeFiStakingSubscription = 80,

    /// <summary>
    /// VoteRedemption
    /// </summary>
    [Obsolete]
    [Map("81")]
    VoteRedemption = 81,

    /// <summary>
    /// DEFI/Staking redemption
    /// </summary>
    [Map("82")]
    DeFiStakingRedemption = 82,

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
    [Obsolete]
    [Map("85")]
    PowMiningYield = 85,

    /// <summary>
    /// CloudMiningPay
    /// </summary>
    [Obsolete]
    [Map("86")]
    CloudMiningPay = 86,

    /// <summary>
    /// CloudMiningYield
    /// </summary>
    [Obsolete]
    [Map("87")]
    CloudMiningYield = 87,

    /// <summary>
    /// Subsidy
    /// </summary>
    [Obsolete]
    [Map("88")]
    Subsidy = 88,

    /// <summary>
    /// Deposit yield
    /// </summary>
    [Map("89")]
    DepositYield = 89,

    /// <summary>
    /// StakingSubscription
    /// </summary>
    [Obsolete]
    [Map("90")]
    StakingSubscription = 90,

    /// <summary>
    /// StakingRedemption91
    /// </summary>
    [Obsolete]
    [Map("91")]
    StakingRedemption91 = 91,

    /// <summary>
    /// AddCollateral
    /// </summary>
    [Obsolete]
    [Map("92")]
    AddCollateral = 92,

    /// <summary>
    /// RedeemCollateral
    /// </summary>
    [Obsolete]
    [Map("93")]
    RedeemCollateral = 93,

    /// <summary>
    /// Investment
    /// </summary>
    [Obsolete]
    [Map("94")]
    Investment = 94,

    /// <summary>
    /// BorrowerBorrows
    /// </summary>
    [Obsolete]
    [Map("95")]
    BorrowerBorrows = 95,

    /// <summary>
    /// PrincipalTransferredIn
    /// </summary>
    [Obsolete]
    [Map("96")]
    PrincipalTransferredIn = 96,

    /// <summary>
    /// BorrowerTransferredLoanOut
    /// </summary>
    [Obsolete]
    [Map("97")]
    BorrowerTransferredLoanOut = 97,

    /// <summary>
    /// BorrowerTransferredInterestOut
    /// </summary>
    [Obsolete]
    [Map("98")]
    BorrowerTransferredInterestOut = 98,

    /// <summary>
    /// InvestorTransferredInterestIn
    /// </summary>
    [Obsolete]
    [Map("99")]
    InvestorTransferredInterestIn = 99,

    /// <summary>
    /// PrepaymentPenaltyTransferredIn
    /// </summary>
    [Obsolete]
    [Map("102")]
    PrepaymentPenaltyTransferredIn = 102,

    /// <summary>
    /// PrepaymentPenaltyTransferredOut
    /// </summary>
    [Obsolete]
    [Map("103")]
    PrepaymentPenaltyTransferredOut = 103,

    /// <summary>
    /// MortgageFeeTransferredIn
    /// </summary>
    [Obsolete]
    [Map("104")]
    MortgageFeeTransferredIn = 104,

    /// <summary>
    /// MortgageFeeTransferredOut
    /// </summary>
    [Obsolete]
    [Map("105")]
    MortgageFeeTransferredOut = 105,

    /// <summary>
    /// OverdueFeeTransferredIn
    /// </summary>
    [Obsolete]
    [Map("106")]
    OverdueFeeTransferredIn = 106,

    /// <summary>
    /// OverdueFeeTransferredOut
    /// </summary>
    [Obsolete]
    [Map("107")]
    OverdueFeeTransferredOut = 107,

    /// <summary>
    /// OverdueInterestTransferredOut
    /// </summary>
    [Obsolete]
    [Map("108")]
    OverdueInterestTransferredOut = 108,

    /// <summary>
    /// OverdueInterestTransferredIn
    /// </summary>
    [Obsolete]
    [Map("109")]
    OverdueInterestTransferredIn = 109,

    /// <summary>
    /// CollateralForClosedPositionTransferredIn
    /// </summary>
    [Obsolete]
    [Map("110")]
    CollateralForClosedPositionTransferredIn = 110,

    /// <summary>
    /// CollateralForClosedPositionTransferredOut
    /// </summary>
    [Obsolete]
    [Map("111")]
    CollateralForClosedPositionTransferredOut = 111,

    /// <summary>
    /// CollateralForLiquidationTransferredIn
    /// </summary>
    [Obsolete]
    [Map("112")]
    CollateralForLiquidationTransferredIn = 112,

    /// <summary>
    /// CollateralForLiquidationTransferredOut
    /// </summary>
    [Obsolete]
    [Map("113")]
    CollateralForLiquidationTransferredOut = 113,

    /// <summary>
    /// InsuranceFundTransferredIn
    /// </summary>
    [Obsolete]
    [Map("114")]
    InsuranceFundTransferredIn = 114,

    /// <summary>
    /// InsuranceFundTransferredOut
    /// </summary>
    [Obsolete]
    [Map("115")]
    InsuranceFundTransferredOut = 115,

    /// <summary>
    /// [Fiat] Place an order
    /// </summary>
    [Map("116")]
    PlaceAnOrder = 116,

    /// <summary>
    /// [Fiat] Fulfill an order
    /// </summary>
    [Map("117")]
    FulfillAnOrder = 117,

    /// <summary>
    /// [Fiat] Cancel an order
    /// </summary>
    [Map("118")]
    CancelAnOrder = 118,

    /// <summary>
    /// MerchantsUnlockDeposit
    /// </summary>
    [Obsolete]
    [Map("119")]
    MerchantsUnlockDeposit = 119,

    /// <summary>
    /// MerchantsAddDeposit
    /// </summary>
    [Obsolete]
    [Map("120")]
    MerchantsAddDeposit = 120,

    /// <summary>
    /// FiatgatewayPlaceAnOrder
    /// </summary>
    [Obsolete]
    [Map("121")]
    FiatgatewayPlaceAnOrder = 121,

    /// <summary>
    /// FiatgatewayCancelAnOrder
    /// </summary>
    [Obsolete]
    [Map("122")]
    FiatgatewayCancelAnOrder = 122,

    /// <summary>
    /// FiatgatewayFulfillAnOrder
    /// </summary>
    [Obsolete]
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
    [Obsolete]
    [Map("125")]
    ManualDeposit = 125,

    /// <summary>
    /// InterestDeposit
    /// </summary>
    [Obsolete]
    [Map("126")]
    InterestDeposit = 126,

    /// <summary>
    /// InvestmentFeeTransferredIn
    /// </summary>
    [Obsolete]
    [Map("127")]
    InvestmentFeeTransferredIn = 127,

    /// <summary>
    /// InvestmentFeeTransferredOut
    /// </summary>
    [Obsolete]
    [Map("128")]
    InvestmentFeeTransferredOut = 128,

    /// <summary>
    /// RewardsTransferredIn
    /// </summary>
    [Obsolete]
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
    [Obsolete]
    [Map("136")]
    Convert = 136,

    /// <summary>
    /// [ETH Staking] Subscription
    /// </summary>
    [Map("137")]
    EthStakingSubscription = 137,

    /// <summary>
    /// [ETH Staking] Swapping
    /// </summary>
    [Map("138")]
    EthStakingSwapping = 138,

    /// <summary>
    /// [ETH Staking] Earnings
    /// </summary>
    [Map("139")]
    EthStakingEarnings = 139,

    /// <summary>
    /// SystemReverse
    /// </summary>
    [Obsolete]
    [Map("143")]
    SystemReverse = 143,

    /// <summary>
    /// TransferOutOfUnifiedAccountReserve
    /// </summary>
    [Obsolete]
    [Map("144")]
    TransferOutOfUnifiedAccountReserve = 144,

    /// <summary>
    /// RewardExpired
    /// </summary>
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
    [Map("153")]
    JoinerReward = 153,

    /// <summary>
    /// MysteryBoxReward
    /// </summary>
    [Obsolete]
    [Map("154")]
    MysteryBoxReward = 154,

    /// <summary>
    /// RewardsWithdraw
    /// </summary>
    [Obsolete]
    [Map("155")]
    RewardsWithdraw = 155,

    /// <summary>
    /// FeeRebate156
    /// </summary>
    [Obsolete]
    [Map("156")]
    FeeRebate156 = 156,

    /// <summary>
    /// CollectedCrypto
    /// </summary>
    [Obsolete]
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
    [Obsolete]
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
    /// Jumpstart Pay
    /// </summary>
    [Map("174")]
    JumpstartPay = 174,

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
    [Obsolete]
    [Map("182")]
    FeedbackBounty = 182,

    /// <summary>
    /// InviteFriendsRewards
    /// </summary>
    [Obsolete]
    [Map("183")]
    InviteFriendsRewards = 183,

    /// <summary>
    /// DivideTheRewardPool
    /// </summary>
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
    [Map("211")]
    WinCryptoWithSatoshi = 211,

    /// <summary>
    /// [Flexible loan] MultiCollateralLoanCollateralLocked
    /// </summary>
    [Map("212")]
    FlexibleLoanMultiCollateralLoanCollateralLocked = 212,

    /// <summary>
    /// CollateralTransferedOutFromUserAccount
    /// </summary>
    [Obsolete]
    [Map("213")]
    CollateralTransferedOutFromUserAccount = 213,

    /// <summary>
    /// CollateralReturnedToUsers
    /// </summary>
    [Obsolete]
    [Map("214")]
    CollateralReturnedToUsers = 214,

    /// <summary>
    /// [Flexible loan]  MultiCollateralLoanCollateralReleased
    /// </summary>
    [Map("215")]
    FlexibleLoanMultiCollateralLoanCollateralReleased = 215,

    /// <summary>
    /// LoanTransferredToUserAccount
    /// </summary>
    [Obsolete]
    [Map("216")]
    LoanTransferredToUserAccount = 216,

    /// <summary>
    /// [Flexible loan] MultiCollateralLoanBorrowed
    /// </summary>
    [Map("217")]
    FlexibleLoanMultiCollateralLoanBorrowed = 217,

    /// <summary>
    /// [Flexible loan] MultiCollateralLoanRepaid
    /// </summary>
    [Map("218")]
    FlexibleLoanMultiCollateralLoanRepaid = 218,

    /// <summary>
    /// RepaymentTransferredFromUserAccount
    /// </summary>
    [Obsolete]
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
    [Obsolete]
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
    [Obsolete]
    [Map("230")]
    TokenMigrationCompleted = 230,

    /// <summary>
    /// [Flexible loan] SubsidizedInterestReceived
    /// </summary>
    [Map("232")]
    FlexibleLoanSubsidizedInterestReceived = 232,

    /// <summary>
    /// BrokerRebateCompensation
    /// </summary>
    [Map("233")]
    BrokerRebateCompensation = 233,
    
    /// <summary>
    /// Snowball subscribe
    /// </summary>
    [Map("240")]
    SnowballSubscribe = 240,

    /// <summary>
    /// Snowball refund
    /// </summary>
    [Map("241")]
    SnowballRefund = 241,

    /// <summary>
    /// Snowball profit
    /// </summary>
    [Map("242")]
    SnowballProfit = 242,

    /// <summary>
    /// Snowball trading failed
    /// </summary>
    [Map("243")]
    SnowballTradingFailed = 243,

    /// <summary>
    /// Seagull subscribe
    /// </summary>
    [Map("249")]
    SeagullSubscribe = 249,

    /// <summary>
    /// Seagull collection
    /// </summary>
    [Map("250")]
    SeagullCollection = 250,

    /// <summary>
    /// Seagull profit
    /// </summary>
    [Map("251")]
    SeagullProfit = 251,

    /// <summary>
    /// Seagull refund
    /// </summary>
    [Map("252")]
    SeagullRefund = 252,

    /// <summary>
    /// Strategy bots profit share
    /// </summary>
    [Map("263")]
    StrategyBotsProfitShare = 263,

    /// <summary>
    /// Signal revenue
    /// </summary>
    [Map("265")]
    SignalRevenue = 265,

    /// <summary>
    /// SPOT lead trading profit share
    /// </summary>
    [Map("266")]
    SpotLeadTradingProfitShare = 266,

    /// <summary>
    /// DCD broker transfer
    /// </summary>
    [Map("270")]
    DCDBrokerTransfer = 270,

    /// <summary>
    /// DCD broker rebate
    /// </summary>
    [Map("271")]
    DCDBrokerRebate = 233,

    /// <summary>
    /// [Convert] Buy Crypto/Fiat
    /// </summary>
    [Map("272")]
    ConvertBuyCryptoFiat = 272,

    /// <summary>
    /// [Convert] Sell Crypto/Fiat
    /// </summary>
    [Map("273")]
    ConvertSellCryptoFiat = 273,

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

    /// <summary>
    /// Affiliate recommendation commission
    /// </summary>
    [Map("299")]
    AffiliateRecommendationCommission = 299,

    /// <summary>
    /// Fee discount rebate
    /// </summary>
    [Map("300")]
    FeeDiscountRebate = 300,

    /// <summary>
    /// Snowball market maker transfer
    /// </summary>
    [Map("303")]
    SnowballMarketMakerTransfer = 303,

    /// <summary>
    /// [Simple Earn Fixed] Order submission
    /// </summary>
    [Map("304")]
    [Obsolete]
    ObsoleteSimpleEarnFixedOrderSubmission = 304,

    /// <summary>
    /// [Simple Earn Fixed] Order redemption
    /// </summary>
    [Map("305")]
    [Obsolete]
    ObsoleteSimpleEarnFixedOrderRedemption = 305,

    /// <summary>
    /// [Simple Earn Fixed] Principal distribution
    /// </summary>
    [Map("306")]
    [Obsolete]
    ObsoleteSimpleEarnFixedPrincipalDistribution = 306,

    /// <summary>
    /// [Simple Earn Fixed] Interest distribution (early termination compensation)
    /// </summary>
    [Map("307")]
    [Obsolete]
    ObsoleteSimpleEarnFixedInterestDistributionEarlyTerminationCompensation = 307,

    /// <summary>
    /// [Simple Earn Fixed] Interest distribution
    /// </summary>
    [Map("308")]
    [Obsolete]
    ObsoleteSimpleEarnFixedInterestDistribution = 308,

    /// <summary>
    /// [Simple Earn Fixed] Interest distribution (extension compensation)
    /// </summary>
    [Map("309")]
    [Obsolete]
    ObsoleteSimpleEarnFixedInterestDistributionExtensionCompensation = 309,

    /// <summary>
    /// Crypto dust auto-transfer in
    /// </summary>
    [Map("311")]
    CryptoDustAutoTransferIn = 311,

    /// <summary>
    /// Sent by gift
    /// </summary>
    [Map("313")]
    SentByGift = 313,

    /// <summary>
    /// Received from gift
    /// </summary>
    [Map("314")]
    ReceivedFromGift = 314,

    /// <summary>
    /// Refunded from gift
    /// </summary>
    [Map("315")]
    RefundedFromGift = 315,

    /// <summary>
    /// [SOL staking] Send Liquidity Staking Token reward
    /// </summary>
    [Map("315")]
    SolStakingSendLiquidityStakingTokenReward = 315,

    /// <summary>
    /// [SOL staking] Subscribe Liquidity Staking Token staking
    /// </summary>
    [Map("329")]
    SolstakingSubscribeLiquidityStakingTokenStaking = 329,

    /// <summary>
    /// [SOL staking] Mint Liquidity Staking Token
    /// </summary>
    [Map("330")]
    SolStakingMintLiquidityStakingToken = 330,

    /// <summary>
    /// [SOL staking] Redeem Liquidity Staking Token order
    /// </summary>
    [Map("331")]
    SolStakingRedeemLiquidityStakingTokenOrder = 331,

    /// <summary>
    /// [SOL staking] Settle Liquidity Staking Token order
    /// </summary>
    [Map("332")]
    SolStakingSettleLiquidityStakingTokenOrder = 332,

    /// <summary>
    /// Trial fund reward
    /// </summary>
    [Map("333")]
    TrialFundReward = 333,

    /// <summary>
    /// [Credit line] Loan Forced Repayment
    /// </summary>
    [Map("336")]
    CreditLineLoanForcedRepayment = 336,

    /// <summary>
    /// [Credit line] Forced Repayment Refund
    /// </summary>
    [Map("338")]
    CreditLineForcedRepaymentRefund = 338,

    /// <summary>
    /// [Simple Earn Fixed] Order submission
    /// </summary>
    [Map("339")]
    SimpleEarnFixedOrderSubmission = 339,

    /// <summary>
    /// [Simple Earn Fixed] Order failure refund
    /// </summary>
    [Map("340")]
    SimpleEarnFixedOrderFailureRefund = 340,

    /// <summary>
    /// [Simple Earn Fixed] Redemption
    /// </summary>
    [Map("341")]
    SimpleEarnFixedRedemption = 341,

    /// <summary>
    /// [Simple Earn Fixed] Principal
    /// </summary>
    [Map("342")]
    SimpleEarnFixedPrincipal = 342,

    /// <summary>
    /// [Simple Earn Fixed] Interest
    /// </summary>
    [Map("343")]
    SimpleEarnFixedInterest = 343,

    /// <summary>
    /// [Simple Earn Fixed] Compensatory interest
    /// </summary>
    [Map("344")]
    SimpleEarnFixedCompensatoryInterest = 344,

    /// <summary>
    /// [Institutional Loan] Principal repayment
    /// </summary>
    [Map("345")]
    InstitutionalLoanPrincipalRepayment = 345,

    /// <summary>
    /// [Institutional Loan] Interest repayment
    /// </summary>
    [Map("346")]
    InstitutionalLoanInterestRepayment = 346,

    /// <summary>
    /// [Institutional Loan] Overdue penalty
    /// </summary>
    [Map("347")]
    InstitutionalLoanOverduePenalty = 347,

    /// <summary>
    /// [BTC staking] Subscription
    /// </summary>
    [Map("348")]
    BtcStakingSubscription = 348,

    /// <summary>
    /// [BTC staking] Redemption
    /// </summary>
    [Map("349")]
    BtcStakingRedemption = 349,

    /// <summary>
    /// [BTC staking] Earnings
    /// </summary>
    [Map("350")]
    BtcStakingEarnings = 350,

    /// <summary>
    /// [Institutional Loan] Loan disbursement
    /// </summary>
    [Map("351")]
    InstitutionalLoanDisbursement = 351,

    /// <summary>
    /// Copy and bot rewards
    /// </summary>
    [Map("354")]
    CopyAndBotRewards = 354,

    /// <summary>
    /// Deposit from closed sub-account
    /// </summary>
    [Map("361")]
    DepositFromClosedSubAccount = 361,

    /// <summary>
    /// Asset segregation
    /// </summary>
    [Map("372")]
    AssetSegregation = 372,

    /// <summary>
    /// Asset release
    /// </summary>
    [Map("373")]
    AssetRelease = 373,
}