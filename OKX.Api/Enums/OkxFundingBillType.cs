namespace OKX.Api.Enums;

/// <summary>
/// OKX Funding Bill Type
/// </summary>
public enum OkxFundingBillType
{
    /// <summary>
    /// Deposit
    /// </summary>
    [Label("1")]
    Deposit,

    /// <summary>
    /// Withdrawal
    /// </summary>
    [Label("2")]
    Withdrawal,

    /// <summary>
    /// CanceledWithdrawal
    /// </summary>
    [Label("13")]
    CanceledWithdrawal,

    /// <summary>
    /// TransferToFutures
    /// </summary>
    [Label("18")]
    TransferToFutures,

    /// <summary>
    /// TransferFromFutures
    /// </summary>
    [Label("19")]
    TransferFromFutures,

    /// <summary>
    /// TransferToSubAccount
    /// </summary>
    [Label("20")]
    TransferToSubAccount,

    /// <summary>
    /// TransferFromSubAccount
    /// </summary>
    [Label("21")]
    TransferFromSubAccount,

    /// <summary>
    /// TransferOutFromSubToMasterAccount
    /// </summary>
    [Label("22")]
    TransferOutFromSubToMasterAccount,

    /// <summary>
    /// TransferInFromMasterToSubAccount
    /// </summary>
    [Label("23")]
    TransferInFromMasterToSubAccount,

    /// <summary>
    /// ManuallyClaimedAirdrop
    /// </summary>
    [Label("28")]
    ManuallyClaimedAirdrop,

    /// <summary>
    /// TransferToMargin
    /// </summary>
    [Label("33")]
    TransferToMargin,

    /// <summary>
    /// TransferFromMargin
    /// </summary>
    [Label("34")]
    TransferFromMargin,

    /// <summary>
    /// TransferToSpot
    /// </summary>
    [Label("37")]
    TransferToSpot,

    /// <summary>
    /// TransferFromSpot
    /// </summary>
    [Label("38")]
    TransferFromSpot,

    /// <summary>
    /// TradingFeesSettledByLoyaltyPoints
    /// </summary>
    [Label("41")]
    TradingFeesSettledByLoyaltyPoints,

    /// <summary>
    /// LoyaltyPointsPurchase
    /// </summary>
    [Label("42")]
    LoyaltyPointsPurchase,

    /// <summary>
    /// SystemReversal
    /// </summary>
    [Label("47")]
    SystemReversal,

    /// <summary>
    /// EventReward
    /// </summary>
    [Label("48")]
    EventReward,

    /// <summary>
    /// EventGiveaway
    /// </summary>
    [Label("49")]
    EventGiveaway,

    /// <summary>
    /// ReceivedFromAppointments
    /// </summary>
    [Label("50")]
    ReceivedFromAppointments,

    /// <summary>
    /// DeductedFromAppointments
    /// </summary>
    [Label("51")]
    DeductedFromAppointments,

    /// <summary>
    /// RedPacketSent
    /// </summary>
    [Label("52")]
    RedPacketSent,

    /// <summary>
    /// RedPacketSnatched
    /// </summary>
    [Label("53")]
    RedPacketSnatched,

    /// <summary>
    /// RedPacketRefunded
    /// </summary>
    [Label("54")]
    RedPacketRefunded,

    /// <summary>
    /// TransferToPerpetual
    /// </summary>
    [Label("55")]
    TransferToPerpetual,

    /// <summary>
    /// TransferFromPerpetual
    /// </summary>
    [Label("56")]
    TransferFromPerpetual,

    /// <summary>
    /// TransferFromHedgingAccount
    /// </summary>
    [Label("59")]
    TransferFromHedgingAccount,

    /// <summary>
    /// TransferToHedgingAccount
    /// </summary>
    [Label("60")]
    TransferToHedgingAccount,

    /// <summary>
    /// Conversion
    /// </summary>
    [Label("61")]
    Conversion,

    /// <summary>
    /// TransferToOptions
    /// </summary>
    [Label("63")]
    TransferToOptions,

    /// <summary>
    /// TransferFromOptions
    /// </summary>
    [Label("62")]
    TransferFromOptions,

    /// <summary>
    /// ClaimRebateCard
    /// </summary>
    [Label("68")]
    ClaimRebateCard,

    /// <summary>
    /// DistributeRebateCard
    /// </summary>
    [Label("69")]
    DistributeRebateCard,

    /// <summary>
    /// TokenReceived
    /// </summary>
    [Label("72")]
    TokenReceived,

    /// <summary>
    /// TokenGivenAway
    /// </summary>
    [Label("73")]
    TokenGivenAway,

    /// <summary>
    /// TokenRefunded
    /// </summary>
    [Label("74")]
    TokenRefunded,

    /// <summary>
    /// SubscriptionToSavings
    /// </summary>
    [Label("75")]
    SubscriptionToSavings,

    /// <summary>
    /// RedemptionToSavings
    /// </summary>
    [Label("76")]
    RedemptionToSavings,

    /// <summary>
    /// Distribute
    /// </summary>
    [Label("77")]
    Distribute,

    /// <summary>
    /// LockUp
    /// </summary>
    [Label("78")]
    LockUp,

    /// <summary>
    /// NodeVoting
    /// </summary>
    [Label("79")]
    NodeVoting,

    /// <summary>
    /// Staking80
    /// </summary>
    [Label("80")]
    Staking80,

    /// <summary>
    /// VoteRedemption
    /// </summary>
    [Label("81")]
    VoteRedemption,

    /// <summary>
    /// StakingRedemption82
    /// </summary>
    [Label("82")]
    StakingRedemption82,

    /// <summary>
    /// StakingYield
    /// </summary>
    [Label("83")]
    StakingYield,

    /// <summary>
    /// ViolationFee
    /// </summary>
    [Label("84")]
    ViolationFee,

    /// <summary>
    /// PowMiningYield
    /// </summary>
    [Label("85")]
    PowMiningYield,

    /// <summary>
    /// CloudMiningPay
    /// </summary>
    [Label("86")]
    CloudMiningPay,

    /// <summary>
    /// CloudMiningYield
    /// </summary>
    [Label("87")]
    CloudMiningYield,

    /// <summary>
    /// Subsidy
    /// </summary>
    [Label("88")]
    Subsidy,

    /// <summary>
    /// Staking89
    /// </summary>
    [Label("89")]
    Staking89,

    /// <summary>
    /// StakingSubscription
    /// </summary>
    [Label("90")]
    StakingSubscription,

    /// <summary>
    /// StakingRedemption91
    /// </summary>
    [Label("91")]
    StakingRedemption91,

    /// <summary>
    /// AddCollateral
    /// </summary>
    [Label("92")]
    AddCollateral,

    /// <summary>
    /// RedeemCollateral
    /// </summary>
    [Label("93")]
    RedeemCollateral,

    /// <summary>
    /// Investment
    /// </summary>
    [Label("94")]
    Investment,

    /// <summary>
    /// BorrowerBorrows
    /// </summary>
    [Label("95")]
    BorrowerBorrows,

    /// <summary>
    /// PrincipalTransferredIn
    /// </summary>
    [Label("96")]
    PrincipalTransferredIn,

    /// <summary>
    /// BorrowerTransferredLoanOut
    /// </summary>
    [Label("97")]
    BorrowerTransferredLoanOut,

    /// <summary>
    /// BorrowerTransferredInterestOut
    /// </summary>
    [Label("98")]
    BorrowerTransferredInterestOut,

    /// <summary>
    /// InvestorTransferredInterestIn
    /// </summary>
    [Label("99")]
    InvestorTransferredInterestIn,

    /// <summary>
    /// PrepaymentPenaltyTransferredIn
    /// </summary>
    [Label("102")]
    PrepaymentPenaltyTransferredIn,

    /// <summary>
    /// PrepaymentPenaltyTransferredOut
    /// </summary>
    [Label("103")]
    PrepaymentPenaltyTransferredOut,

    /// <summary>
    /// MortgageFeeTransferredIn
    /// </summary>
    [Label("104")]
    MortgageFeeTransferredIn,

    /// <summary>
    /// MortgageFeeTransferredOut
    /// </summary>
    [Label("105")]
    MortgageFeeTransferredOut,

    /// <summary>
    /// OverdueFeeTransferredIn
    /// </summary>
    [Label("106")]
    OverdueFeeTransferredIn,

    /// <summary>
    /// OverdueFeeTransferredOut
    /// </summary>
    [Label("107")]
    OverdueFeeTransferredOut,

    /// <summary>
    /// OverdueInterestTransferredOut
    /// </summary>
    [Label("108")]
    OverdueInterestTransferredOut,

    /// <summary>
    /// OverdueInterestTransferredIn
    /// </summary>
    [Label("109")]
    OverdueInterestTransferredIn,

    /// <summary>
    /// CollateralForClosedPositionTransferredIn
    /// </summary>
    [Label("110")]
    CollateralForClosedPositionTransferredIn,

    /// <summary>
    /// CollateralForClosedPositionTransferredOut
    /// </summary>
    [Label("111")]
    CollateralForClosedPositionTransferredOut,

    /// <summary>
    /// CollateralForLiquidationTransferredIn
    /// </summary>
    [Label("112")]
    CollateralForLiquidationTransferredIn,

    /// <summary>
    /// CollateralForLiquidationTransferredOut
    /// </summary>
    [Label("113")]
    CollateralForLiquidationTransferredOut,

    /// <summary>
    /// InsuranceFundTransferredIn
    /// </summary>
    [Label("114")]
    InsuranceFundTransferredIn,

    /// <summary>
    /// InsuranceFundTransferredOut
    /// </summary>
    [Label("115")]
    InsuranceFundTransferredOut,

    /// <summary>
    /// PlaceAnOrder
    /// </summary>
    [Label("116")]
    PlaceAnOrder,

    /// <summary>
    /// FulfillAnOrder
    /// </summary>
    [Label("117")]
    FulfillAnOrder,

    /// <summary>
    /// CancelAnOrder
    /// </summary>
    [Label("118")]
    CancelAnOrder,

    /// <summary>
    /// MerchantsUnlockDeposit
    /// </summary>
    [Label("119")]
    MerchantsUnlockDeposit,

    /// <summary>
    /// MerchantsAddDeposit
    /// </summary>
    [Label("120")]
    MerchantsAddDeposit,

    /// <summary>
    /// FiatgatewayPlaceAnOrder
    /// </summary>
    [Label("121")]
    FiatgatewayPlaceAnOrder,

    /// <summary>
    /// FiatgatewayCancelAnOrder
    /// </summary>
    [Label("122")]
    FiatgatewayCancelAnOrder,

    /// <summary>
    /// FiatgatewayFulfillAnOrder
    /// </summary>
    [Label("123")]
    FiatgatewayFulfillAnOrder,

    /// <summary>
    /// JumpstartUnlocking
    /// </summary>
    [Label("124")]
    JumpstartUnlocking,

    /// <summary>
    /// ManualDeposit
    /// </summary>
    [Label("125")]
    ManualDeposit,

    /// <summary>
    /// InterestDeposit
    /// </summary>
    [Label("126")]
    InterestDeposit,

    /// <summary>
    /// InvestmentFeeTransferredIn
    /// </summary>
    [Label("127")]
    InvestmentFeeTransferredIn,

    /// <summary>
    /// InvestmentFeeTransferredOut
    /// </summary>
    [Label("128")]
    InvestmentFeeTransferredOut,

    /// <summary>
    /// RewardsTransferredIn
    /// </summary>
    [Label("129")]
    RewardsTransferredIn,

    /// <summary>
    /// TransferredFromUnifiedAccount
    /// </summary>
    [Label("130")]
    TransferredFromUnifiedAccount,

    /// <summary>
    /// TransferredToUnifiedAccount
    /// </summary>
    [Label("131")]
    TransferredToUnifiedAccount,

    /// <summary>
    /// FrozenByCustomerService
    /// </summary>
    [Label("132")]
    FrozenByCustomerService,

    /// <summary>
    /// UnfrozenByCustomerService
    /// </summary>
    [Label("133")]
    UnfrozenByCustomerService,

    /// <summary>
    /// TransferredByCustomerService
    /// </summary>
    [Label("134")]
    TransferredByCustomerService,

    /// <summary>
    /// CrossChainExchange
    /// </summary>
    [Label("135")]
    CrossChainExchange,

    /// <summary>
    /// Convert
    /// </summary>
    [Label("136")]
    Convert,

    /// <summary>
    /// Eth20Subscription
    /// </summary>
    [Label("137")]
    Eth20Subscription,

    /// <summary>
    /// Eth20Swapping
    /// </summary>
    [Label("138")]
    Eth20Swapping,

    /// <summary>
    /// Eth20Earnings
    /// </summary>
    [Label("139")]
    Eth20Earnings,

    /// <summary>
    /// SystemReverse
    /// </summary>
    [Label("143")]
    SystemReverse,

    /// <summary>
    /// TransferOutOfUnifiedAccountReserve
    /// </summary>
    [Label("144")]
    TransferOutOfUnifiedAccountReserve,

    /// <summary>
    /// RewardExpired
    /// </summary>
    [Label("145")]
    RewardExpired,

    /// <summary>
    /// CustomerFeedback
    /// </summary>
    [Label("146")]
    CustomerFeedback,

    /// <summary>
    /// VestedSushiRewards
    /// </summary>
    [Label("147")]
    VestedSushiRewards,

    /// <summary>
    /// AffiliateCommission
    /// </summary>
    [Label("150")]
    AffiliateCommission,

    /// <summary>
    /// ReferralReward
    /// </summary>
    [Label("151")]
    ReferralReward,

    /// <summary>
    /// BrokerReward
    /// </summary>
    [Label("152")]
    BrokerReward,

    /// <summary>
    /// JoinerReward
    /// </summary>
    [Label("153")]
    JoinerReward,

    /// <summary>
    /// MysteryBoxReward
    /// </summary>
    [Label("154")]
    MysteryBoxReward,

    /// <summary>
    /// RewardsWithdraw
    /// </summary>
    [Label("155")]
    RewardsWithdraw,

    /// <summary>
    /// FeeRebate156
    /// </summary>
    [Label("156")]
    FeeRebate156,

    /// <summary>
    /// CollectedCrypto
    /// </summary>
    [Label("157")]
    CollectedCrypto,

    /// <summary>
    /// DualInvestmentSubscribe
    /// </summary>
    [Label("160")]
    DualInvestmentSubscribe,

    /// <summary>
    /// DualInvestmentCollection
    /// </summary>
    [Label("161")]
    DualInvestmentCollection,

    /// <summary>
    /// DualInvestmentProfit
    /// </summary>
    [Label("162")]
    DualInvestmentProfit,

    /// <summary>
    /// DualInvestmentRefund
    /// </summary>
    [Label("163")]
    DualInvestmentRefund,

    /// <summary>
    /// NewYearRewards2022
    /// </summary>
    [Label("169")]
    NewYearRewards2022,

    /// <summary>
    /// SubAffiliateCommission
    /// </summary>
    [Label("172")]
    SubAffiliateCommission,

    /// <summary>
    /// FeeRebate173
    /// </summary>
    [Label("173")]
    FeeRebate173,

    /// <summary>
    /// Pay
    /// </summary>
    [Label("174")]
    Pay,

    /// <summary>
    /// LockedCollateral
    /// </summary>
    [Label("175")]
    LockedCollateral,

    /// <summary>
    /// Loan
    /// </summary>
    [Label("176")]
    Loan,

    /// <summary>
    /// AddedCollateral
    /// </summary>
    [Label("177")]
    AddedCollateral,

    /// <summary>
    /// ReturnedCollateral
    /// </summary>
    [Label("178")]
    ReturnedCollateral,

    /// <summary>
    /// Repayment
    /// </summary>
    [Label("179")]
    Repayment,

    /// <summary>
    /// UnlockedCollateral
    /// </summary>
    [Label("180")]
    UnlockedCollateral,

    /// <summary>
    /// AirdropPayment
    /// </summary>
    [Label("181")]
    AirdropPayment,

    /// <summary>
    /// FeedbackBounty
    /// </summary>
    [Label("182")]
    FeedbackBounty,

    /// <summary>
    /// InviteFriendsRewards
    /// </summary>
    [Label("183")]
    InviteFriendsRewards,

    /// <summary>
    /// DivideTheRewardPool
    /// </summary>
    [Label("184")]
    DivideTheRewardPool,

    /// <summary>
    /// BrokerConvertReward
    /// </summary>
    [Label("185")]
    BrokerConvertReward,

    /// <summary>
    /// Freeeth
    /// </summary>
    [Label("186")]
    Freeeth,

    /// <summary>
    /// ConvertTransfer
    /// </summary>
    [Label("187")]
    ConvertTransfer,

    /// <summary>
    /// SlotAuctionConversion
    /// </summary>
    [Label("188")]
    SlotAuctionConversion,

    /// <summary>
    /// MysteryBoxBonus
    /// </summary>
    [Label("189")]
    MysteryBoxBonus,

    /// <summary>
    /// CardPaymentBuy
    /// </summary>
    [Label("193")]
    CardPaymentBuy,

    /// <summary>
    /// UntradableAssetWithdrawal
    /// </summary>
    [Label("195")]
    UntradableAssetWithdrawal,

    /// <summary>
    /// UntradableAssetWithdrawalRevoked
    /// </summary>
    [Label("196")]
    UntradableAssetWithdrawalRevoked,

    /// <summary>
    /// UntradableAssetDeposit
    /// </summary>
    [Label("197")]
    UntradableAssetDeposit,

    /// <summary>
    /// UntradableAssetCollectionReduce
    /// </summary>
    [Label("198")]
    UntradableAssetCollectionReduce,

    /// <summary>
    /// UntradableAssetCollectionIncrease
    /// </summary>
    [Label("199")]
    UntradableAssetCollectionIncrease,

    /// <summary>
    /// Buy
    /// </summary>
    [Label("200")]
    Buy,

    /// <summary>
    /// PriceLockSubscribe
    /// </summary>
    [Label("202")]
    PriceLockSubscribe,

    /// <summary>
    /// PriceLockCollection
    /// </summary>
    [Label("203")]
    PriceLockCollection,

    /// <summary>
    /// PriceLockProfit
    /// </summary>
    [Label("204")]
    PriceLockProfit,

    /// <summary>
    /// PriceLockRefund
    /// </summary>
    [Label("205")]
    PriceLockRefund,

    /// <summary>
    /// DualInvestmentLiteSubscribe
    /// </summary>
    [Label("207")]
    DualInvestmentLiteSubscribe,

    /// <summary>
    /// DualInvestmentLiteCollection
    /// </summary>
    [Label("208")]
    DualInvestmentLiteCollection,

    /// <summary>
    /// DualInvestmentLiteProfit
    /// </summary>
    [Label("209")]
    DualInvestmentLiteProfit,

    /// <summary>
    /// DualInvestmentLiteRefund
    /// </summary>
    [Label("210")]
    DualInvestmentLiteRefund,

    /// <summary>
    /// WinCryptoWithSatoshi
    /// </summary>
    [Label("211")]
    WinCryptoWithSatoshi,

    /// <summary>
    /// MultiCollateralLoanCollateralLocked
    /// </summary>
    [Label("212")]
    MultiCollateralLoanCollateralLocked,

    /// <summary>
    /// CollateralTransferedOutFromUserAccount
    /// </summary>
    [Label("213")]
    CollateralTransferedOutFromUserAccount,

    /// <summary>
    /// CollateralReturnedToUsers
    /// </summary>
    [Label("214")]
    CollateralReturnedToUsers,

    /// <summary>
    /// MultiCollateralLoanCollateralReleased
    /// </summary>
    [Label("215")]
    MultiCollateralLoanCollateralReleased,

    /// <summary>
    /// LoanTransferredToUserAccount
    /// </summary>
    [Label("216")]
    LoanTransferredToUserAccount,

    /// <summary>
    /// MultiCollateralLoanBorrowed
    /// </summary>
    [Label("217")]
    MultiCollateralLoanBorrowed,

    /// <summary>
    /// MultiCollateralLoanRepaid
    /// </summary>
    [Label("218")]
    MultiCollateralLoanRepaid,

    /// <summary>
    /// RepaymentTransferredFromUserAccount
    /// </summary>
    [Label("219")]
    RepaymentTransferredFromUserAccount,

    /// <summary>
    /// DelistedCrypto
    /// </summary>
    [Label("220")]
    DelistedCrypto,

    /// <summary>
    /// BlockchainWithdrawalFee
    /// </summary>
    [Label("221")]
    BlockchainWithdrawalFee,

    /// <summary>
    /// WithdrawalFeeRefund
    /// </summary>
    [Label("222")]
    WithdrawalFeeRefund,

    /// <summary>
    /// ProfitShare
    /// </summary>
    [Label("223")]
    ProfitShare,

    /// <summary>
    /// ServiceFee
    /// </summary>
    [Label("224")]
    ServiceFee,

    /// <summary>
    /// SharkFinSubscribe
    /// </summary>
    [Label("225")]
    SharkFinSubscribe,

    /// <summary>
    /// SharkFinCollection
    /// </summary>
    [Label("226")]
    SharkFinCollection,

    /// <summary>
    /// SharkFinProfit
    /// </summary>
    [Label("227")]
    SharkFinProfit,

    /// <summary>
    /// SharkFinRefund
    /// </summary>
    [Label("228")]
    SharkFinRefund,

    /// <summary>
    /// Airdrop
    /// </summary>
    [Label("229")]
    Airdrop,

    /// <summary>
    /// TokenMigrationCompleted
    /// </summary>
    [Label("230")]
    TokenMigrationCompleted,

    /// <summary>
    /// SubsidizedInterestReceived
    /// </summary>
    [Label("232")]
    SubsidizedInterestReceived,

    /// <summary>
    /// BrokerRebateCompensation
    /// </summary>
    [Label("233")]
    BrokerRebateCompensation,

    /// <summary>
    /// TransferOutTradingSubAccount
    /// </summary>
    [Label("284")]
    TransferOutTradingSubAccount,

    /// <summary>
    /// TransferInTradingSubAccount
    /// </summary>
    [Label("285")]
    TransferInTradingSubAccount,

    /// <summary>
    /// TransferOutCustodyFundingAccount
    /// </summary>
    [Label("286")]
    TransferOutCustodyFundingAccount,

    /// <summary>
    /// TransferInCustodyFundingAccount
    /// </summary>
    [Label("287")]
    TransferInCustodyFundingAccount,

    /// <summary>
    /// CustodyFundDelegation
    /// </summary>
    [Label("288")]
    CustodyFundDelegation,

    /// <summary>
    /// CustodyFundUndelegation
    /// </summary>
    [Label("289")]
    CustodyFundUndelegation,
}