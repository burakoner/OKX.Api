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
    Deposit,

    /// <summary>
    /// Withdrawal
    /// </summary>
    [Map("2")]
    Withdrawal,

    /// <summary>
    /// CanceledWithdrawal
    /// </summary>
    [Map("13")]
    CanceledWithdrawal,

    /// <summary>
    /// TransferToFutures
    /// </summary>
    [Map("18")]
    TransferToFutures,

    /// <summary>
    /// TransferFromFutures
    /// </summary>
    [Map("19")]
    TransferFromFutures,

    /// <summary>
    /// TransferToSubAccount
    /// </summary>
    [Map("20")]
    TransferToSubAccount,

    /// <summary>
    /// TransferFromSubAccount
    /// </summary>
    [Map("21")]
    TransferFromSubAccount,

    /// <summary>
    /// TransferOutFromSubToMasterAccount
    /// </summary>
    [Map("22")]
    TransferOutFromSubToMasterAccount,

    /// <summary>
    /// TransferInFromMasterToSubAccount
    /// </summary>
    [Map("23")]
    TransferInFromMasterToSubAccount,

    /// <summary>
    /// ManuallyClaimedAirdrop
    /// </summary>
    [Map("28")]
    ManuallyClaimedAirdrop,

    /// <summary>
    /// TransferToMargin
    /// </summary>
    [Map("33")]
    TransferToMargin,

    /// <summary>
    /// TransferFromMargin
    /// </summary>
    [Map("34")]
    TransferFromMargin,

    /// <summary>
    /// TransferToSpot
    /// </summary>
    [Map("37")]
    TransferToSpot,

    /// <summary>
    /// TransferFromSpot
    /// </summary>
    [Map("38")]
    TransferFromSpot,

    /// <summary>
    /// TradingFeesSettledByLoyaltyPoints
    /// </summary>
    [Map("41")]
    TradingFeesSettledByLoyaltyPoints,

    /// <summary>
    /// LoyaltyPointsPurchase
    /// </summary>
    [Map("42")]
    LoyaltyPointsPurchase,

    /// <summary>
    /// SystemReversal
    /// </summary>
    [Map("47")]
    SystemReversal,

    /// <summary>
    /// EventReward
    /// </summary>
    [Map("48")]
    EventReward,

    /// <summary>
    /// EventGiveaway
    /// </summary>
    [Map("49")]
    EventGiveaway,

    /// <summary>
    /// ReceivedFromAppointments
    /// </summary>
    [Map("50")]
    ReceivedFromAppointments,

    /// <summary>
    /// DeductedFromAppointments
    /// </summary>
    [Map("51")]
    DeductedFromAppointments,

    /// <summary>
    /// RedPacketSent
    /// </summary>
    [Map("52")]
    RedPacketSent,

    /// <summary>
    /// RedPacketSnatched
    /// </summary>
    [Map("53")]
    RedPacketSnatched,

    /// <summary>
    /// RedPacketRefunded
    /// </summary>
    [Map("54")]
    RedPacketRefunded,

    /// <summary>
    /// TransferToPerpetual
    /// </summary>
    [Map("55")]
    TransferToPerpetual,

    /// <summary>
    /// TransferFromPerpetual
    /// </summary>
    [Map("56")]
    TransferFromPerpetual,

    /// <summary>
    /// TransferFromHedgingAccount
    /// </summary>
    [Map("59")]
    TransferFromHedgingAccount,

    /// <summary>
    /// TransferToHedgingAccount
    /// </summary>
    [Map("60")]
    TransferToHedgingAccount,

    /// <summary>
    /// Conversion
    /// </summary>
    [Map("61")]
    Conversion,

    /// <summary>
    /// TransferToOptions
    /// </summary>
    [Map("63")]
    TransferToOptions,

    /// <summary>
    /// TransferFromOptions
    /// </summary>
    [Map("62")]
    TransferFromOptions,

    /// <summary>
    /// ClaimRebateCard
    /// </summary>
    [Map("68")]
    ClaimRebateCard,

    /// <summary>
    /// DistributeRebateCard
    /// </summary>
    [Map("69")]
    DistributeRebateCard,

    /// <summary>
    /// TokenReceived
    /// </summary>
    [Map("72")]
    TokenReceived,

    /// <summary>
    /// TokenGivenAway
    /// </summary>
    [Map("73")]
    TokenGivenAway,

    /// <summary>
    /// TokenRefunded
    /// </summary>
    [Map("74")]
    TokenRefunded,

    /// <summary>
    /// SubscriptionToSavings
    /// </summary>
    [Map("75")]
    SubscriptionToSavings,

    /// <summary>
    /// RedemptionToSavings
    /// </summary>
    [Map("76")]
    RedemptionToSavings,

    /// <summary>
    /// Distribute
    /// </summary>
    [Map("77")]
    Distribute,

    /// <summary>
    /// LockUp
    /// </summary>
    [Map("78")]
    LockUp,

    /// <summary>
    /// NodeVoting
    /// </summary>
    [Map("79")]
    NodeVoting,

    /// <summary>
    /// Staking80
    /// </summary>
    [Map("80")]
    Staking80,

    /// <summary>
    /// VoteRedemption
    /// </summary>
    [Map("81")]
    VoteRedemption,

    /// <summary>
    /// StakingRedemption82
    /// </summary>
    [Map("82")]
    StakingRedemption82,

    /// <summary>
    /// StakingYield
    /// </summary>
    [Map("83")]
    StakingYield,

    /// <summary>
    /// ViolationFee
    /// </summary>
    [Map("84")]
    ViolationFee,

    /// <summary>
    /// PowMiningYield
    /// </summary>
    [Map("85")]
    PowMiningYield,

    /// <summary>
    /// CloudMiningPay
    /// </summary>
    [Map("86")]
    CloudMiningPay,

    /// <summary>
    /// CloudMiningYield
    /// </summary>
    [Map("87")]
    CloudMiningYield,

    /// <summary>
    /// Subsidy
    /// </summary>
    [Map("88")]
    Subsidy,

    /// <summary>
    /// Staking89
    /// </summary>
    [Map("89")]
    Staking89,

    /// <summary>
    /// StakingSubscription
    /// </summary>
    [Map("90")]
    StakingSubscription,

    /// <summary>
    /// StakingRedemption91
    /// </summary>
    [Map("91")]
    StakingRedemption91,

    /// <summary>
    /// AddCollateral
    /// </summary>
    [Map("92")]
    AddCollateral,

    /// <summary>
    /// RedeemCollateral
    /// </summary>
    [Map("93")]
    RedeemCollateral,

    /// <summary>
    /// Investment
    /// </summary>
    [Map("94")]
    Investment,

    /// <summary>
    /// BorrowerBorrows
    /// </summary>
    [Map("95")]
    BorrowerBorrows,

    /// <summary>
    /// PrincipalTransferredIn
    /// </summary>
    [Map("96")]
    PrincipalTransferredIn,

    /// <summary>
    /// BorrowerTransferredLoanOut
    /// </summary>
    [Map("97")]
    BorrowerTransferredLoanOut,

    /// <summary>
    /// BorrowerTransferredInterestOut
    /// </summary>
    [Map("98")]
    BorrowerTransferredInterestOut,

    /// <summary>
    /// InvestorTransferredInterestIn
    /// </summary>
    [Map("99")]
    InvestorTransferredInterestIn,

    /// <summary>
    /// PrepaymentPenaltyTransferredIn
    /// </summary>
    [Map("102")]
    PrepaymentPenaltyTransferredIn,

    /// <summary>
    /// PrepaymentPenaltyTransferredOut
    /// </summary>
    [Map("103")]
    PrepaymentPenaltyTransferredOut,

    /// <summary>
    /// MortgageFeeTransferredIn
    /// </summary>
    [Map("104")]
    MortgageFeeTransferredIn,

    /// <summary>
    /// MortgageFeeTransferredOut
    /// </summary>
    [Map("105")]
    MortgageFeeTransferredOut,

    /// <summary>
    /// OverdueFeeTransferredIn
    /// </summary>
    [Map("106")]
    OverdueFeeTransferredIn,

    /// <summary>
    /// OverdueFeeTransferredOut
    /// </summary>
    [Map("107")]
    OverdueFeeTransferredOut,

    /// <summary>
    /// OverdueInterestTransferredOut
    /// </summary>
    [Map("108")]
    OverdueInterestTransferredOut,

    /// <summary>
    /// OverdueInterestTransferredIn
    /// </summary>
    [Map("109")]
    OverdueInterestTransferredIn,

    /// <summary>
    /// CollateralForClosedPositionTransferredIn
    /// </summary>
    [Map("110")]
    CollateralForClosedPositionTransferredIn,

    /// <summary>
    /// CollateralForClosedPositionTransferredOut
    /// </summary>
    [Map("111")]
    CollateralForClosedPositionTransferredOut,

    /// <summary>
    /// CollateralForLiquidationTransferredIn
    /// </summary>
    [Map("112")]
    CollateralForLiquidationTransferredIn,

    /// <summary>
    /// CollateralForLiquidationTransferredOut
    /// </summary>
    [Map("113")]
    CollateralForLiquidationTransferredOut,

    /// <summary>
    /// InsuranceFundTransferredIn
    /// </summary>
    [Map("114")]
    InsuranceFundTransferredIn,

    /// <summary>
    /// InsuranceFundTransferredOut
    /// </summary>
    [Map("115")]
    InsuranceFundTransferredOut,

    /// <summary>
    /// PlaceAnOrder
    /// </summary>
    [Map("116")]
    PlaceAnOrder,

    /// <summary>
    /// FulfillAnOrder
    /// </summary>
    [Map("117")]
    FulfillAnOrder,

    /// <summary>
    /// CancelAnOrder
    /// </summary>
    [Map("118")]
    CancelAnOrder,

    /// <summary>
    /// MerchantsUnlockDeposit
    /// </summary>
    [Map("119")]
    MerchantsUnlockDeposit,

    /// <summary>
    /// MerchantsAddDeposit
    /// </summary>
    [Map("120")]
    MerchantsAddDeposit,

    /// <summary>
    /// FiatgatewayPlaceAnOrder
    /// </summary>
    [Map("121")]
    FiatgatewayPlaceAnOrder,

    /// <summary>
    /// FiatgatewayCancelAnOrder
    /// </summary>
    [Map("122")]
    FiatgatewayCancelAnOrder,

    /// <summary>
    /// FiatgatewayFulfillAnOrder
    /// </summary>
    [Map("123")]
    FiatgatewayFulfillAnOrder,

    /// <summary>
    /// JumpstartUnlocking
    /// </summary>
    [Map("124")]
    JumpstartUnlocking,

    /// <summary>
    /// ManualDeposit
    /// </summary>
    [Map("125")]
    ManualDeposit,

    /// <summary>
    /// InterestDeposit
    /// </summary>
    [Map("126")]
    InterestDeposit,

    /// <summary>
    /// InvestmentFeeTransferredIn
    /// </summary>
    [Map("127")]
    InvestmentFeeTransferredIn,

    /// <summary>
    /// InvestmentFeeTransferredOut
    /// </summary>
    [Map("128")]
    InvestmentFeeTransferredOut,

    /// <summary>
    /// RewardsTransferredIn
    /// </summary>
    [Map("129")]
    RewardsTransferredIn,

    /// <summary>
    /// TransferredFromUnifiedAccount
    /// </summary>
    [Map("130")]
    TransferredFromUnifiedAccount,

    /// <summary>
    /// TransferredToUnifiedAccount
    /// </summary>
    [Map("131")]
    TransferredToUnifiedAccount,

    /// <summary>
    /// FrozenByCustomerService
    /// </summary>
    [Map("132")]
    FrozenByCustomerService,

    /// <summary>
    /// UnfrozenByCustomerService
    /// </summary>
    [Map("133")]
    UnfrozenByCustomerService,

    /// <summary>
    /// TransferredByCustomerService
    /// </summary>
    [Map("134")]
    TransferredByCustomerService,

    /// <summary>
    /// CrossChainExchange
    /// </summary>
    [Map("135")]
    CrossChainExchange,

    /// <summary>
    /// Convert
    /// </summary>
    [Map("136")]
    Convert,

    /// <summary>
    /// Eth20Subscription
    /// </summary>
    [Map("137")]
    Eth20Subscription,

    /// <summary>
    /// Eth20Swapping
    /// </summary>
    [Map("138")]
    Eth20Swapping,

    /// <summary>
    /// Eth20Earnings
    /// </summary>
    [Map("139")]
    Eth20Earnings,

    /// <summary>
    /// SystemReverse
    /// </summary>
    [Map("143")]
    SystemReverse,

    /// <summary>
    /// TransferOutOfUnifiedAccountReserve
    /// </summary>
    [Map("144")]
    TransferOutOfUnifiedAccountReserve,

    /// <summary>
    /// RewardExpired
    /// </summary>
    [Map("145")]
    RewardExpired,

    /// <summary>
    /// CustomerFeedback
    /// </summary>
    [Map("146")]
    CustomerFeedback,

    /// <summary>
    /// VestedSushiRewards
    /// </summary>
    [Map("147")]
    VestedSushiRewards,

    /// <summary>
    /// AffiliateCommission
    /// </summary>
    [Map("150")]
    AffiliateCommission,

    /// <summary>
    /// ReferralReward
    /// </summary>
    [Map("151")]
    ReferralReward,

    /// <summary>
    /// BrokerReward
    /// </summary>
    [Map("152")]
    BrokerReward,

    /// <summary>
    /// JoinerReward
    /// </summary>
    [Map("153")]
    JoinerReward,

    /// <summary>
    /// MysteryBoxReward
    /// </summary>
    [Map("154")]
    MysteryBoxReward,

    /// <summary>
    /// RewardsWithdraw
    /// </summary>
    [Map("155")]
    RewardsWithdraw,

    /// <summary>
    /// FeeRebate156
    /// </summary>
    [Map("156")]
    FeeRebate156,

    /// <summary>
    /// CollectedCrypto
    /// </summary>
    [Map("157")]
    CollectedCrypto,

    /// <summary>
    /// DualInvestmentSubscribe
    /// </summary>
    [Map("160")]
    DualInvestmentSubscribe,

    /// <summary>
    /// DualInvestmentCollection
    /// </summary>
    [Map("161")]
    DualInvestmentCollection,

    /// <summary>
    /// DualInvestmentProfit
    /// </summary>
    [Map("162")]
    DualInvestmentProfit,

    /// <summary>
    /// DualInvestmentRefund
    /// </summary>
    [Map("163")]
    DualInvestmentRefund,

    /// <summary>
    /// NewYearRewards2022
    /// </summary>
    [Map("169")]
    NewYearRewards2022,

    /// <summary>
    /// SubAffiliateCommission
    /// </summary>
    [Map("172")]
    SubAffiliateCommission,

    /// <summary>
    /// FeeRebate173
    /// </summary>
    [Map("173")]
    FeeRebate173,

    /// <summary>
    /// Pay
    /// </summary>
    [Map("174")]
    Pay,

    /// <summary>
    /// LockedCollateral
    /// </summary>
    [Map("175")]
    LockedCollateral,

    /// <summary>
    /// Loan
    /// </summary>
    [Map("176")]
    Loan,

    /// <summary>
    /// AddedCollateral
    /// </summary>
    [Map("177")]
    AddedCollateral,

    /// <summary>
    /// ReturnedCollateral
    /// </summary>
    [Map("178")]
    ReturnedCollateral,

    /// <summary>
    /// Repayment
    /// </summary>
    [Map("179")]
    Repayment,

    /// <summary>
    /// UnlockedCollateral
    /// </summary>
    [Map("180")]
    UnlockedCollateral,

    /// <summary>
    /// AirdropPayment
    /// </summary>
    [Map("181")]
    AirdropPayment,

    /// <summary>
    /// FeedbackBounty
    /// </summary>
    [Map("182")]
    FeedbackBounty,

    /// <summary>
    /// InviteFriendsRewards
    /// </summary>
    [Map("183")]
    InviteFriendsRewards,

    /// <summary>
    /// DivideTheRewardPool
    /// </summary>
    [Map("184")]
    DivideTheRewardPool,

    /// <summary>
    /// BrokerConvertReward
    /// </summary>
    [Map("185")]
    BrokerConvertReward,

    /// <summary>
    /// Freeeth
    /// </summary>
    [Map("186")]
    Freeeth,

    /// <summary>
    /// ConvertTransfer
    /// </summary>
    [Map("187")]
    ConvertTransfer,

    /// <summary>
    /// SlotAuctionConversion
    /// </summary>
    [Map("188")]
    SlotAuctionConversion,

    /// <summary>
    /// MysteryBoxBonus
    /// </summary>
    [Map("189")]
    MysteryBoxBonus,

    /// <summary>
    /// CardPaymentBuy
    /// </summary>
    [Map("193")]
    CardPaymentBuy,

    /// <summary>
    /// UntradableAssetWithdrawal
    /// </summary>
    [Map("195")]
    UntradableAssetWithdrawal,

    /// <summary>
    /// UntradableAssetWithdrawalRevoked
    /// </summary>
    [Map("196")]
    UntradableAssetWithdrawalRevoked,

    /// <summary>
    /// UntradableAssetDeposit
    /// </summary>
    [Map("197")]
    UntradableAssetDeposit,

    /// <summary>
    /// UntradableAssetCollectionReduce
    /// </summary>
    [Map("198")]
    UntradableAssetCollectionReduce,

    /// <summary>
    /// UntradableAssetCollectionIncrease
    /// </summary>
    [Map("199")]
    UntradableAssetCollectionIncrease,

    /// <summary>
    /// Buy
    /// </summary>
    [Map("200")]
    Buy,

    /// <summary>
    /// PriceLockSubscribe
    /// </summary>
    [Map("202")]
    PriceLockSubscribe,

    /// <summary>
    /// PriceLockCollection
    /// </summary>
    [Map("203")]
    PriceLockCollection,

    /// <summary>
    /// PriceLockProfit
    /// </summary>
    [Map("204")]
    PriceLockProfit,

    /// <summary>
    /// PriceLockRefund
    /// </summary>
    [Map("205")]
    PriceLockRefund,

    /// <summary>
    /// DualInvestmentLiteSubscribe
    /// </summary>
    [Map("207")]
    DualInvestmentLiteSubscribe,

    /// <summary>
    /// DualInvestmentLiteCollection
    /// </summary>
    [Map("208")]
    DualInvestmentLiteCollection,

    /// <summary>
    /// DualInvestmentLiteProfit
    /// </summary>
    [Map("209")]
    DualInvestmentLiteProfit,

    /// <summary>
    /// DualInvestmentLiteRefund
    /// </summary>
    [Map("210")]
    DualInvestmentLiteRefund,

    /// <summary>
    /// WinCryptoWithSatoshi
    /// </summary>
    [Map("211")]
    WinCryptoWithSatoshi,

    /// <summary>
    /// MultiCollateralLoanCollateralLocked
    /// </summary>
    [Map("212")]
    MultiCollateralLoanCollateralLocked,

    /// <summary>
    /// CollateralTransferedOutFromUserAccount
    /// </summary>
    [Map("213")]
    CollateralTransferedOutFromUserAccount,

    /// <summary>
    /// CollateralReturnedToUsers
    /// </summary>
    [Map("214")]
    CollateralReturnedToUsers,

    /// <summary>
    /// MultiCollateralLoanCollateralReleased
    /// </summary>
    [Map("215")]
    MultiCollateralLoanCollateralReleased,

    /// <summary>
    /// LoanTransferredToUserAccount
    /// </summary>
    [Map("216")]
    LoanTransferredToUserAccount,

    /// <summary>
    /// MultiCollateralLoanBorrowed
    /// </summary>
    [Map("217")]
    MultiCollateralLoanBorrowed,

    /// <summary>
    /// MultiCollateralLoanRepaid
    /// </summary>
    [Map("218")]
    MultiCollateralLoanRepaid,

    /// <summary>
    /// RepaymentTransferredFromUserAccount
    /// </summary>
    [Map("219")]
    RepaymentTransferredFromUserAccount,

    /// <summary>
    /// DelistedCrypto
    /// </summary>
    [Map("220")]
    DelistedCrypto,

    /// <summary>
    /// BlockchainWithdrawalFee
    /// </summary>
    [Map("221")]
    BlockchainWithdrawalFee,

    /// <summary>
    /// WithdrawalFeeRefund
    /// </summary>
    [Map("222")]
    WithdrawalFeeRefund,

    /// <summary>
    /// ProfitShare
    /// </summary>
    [Map("223")]
    ProfitShare,

    /// <summary>
    /// ServiceFee
    /// </summary>
    [Map("224")]
    ServiceFee,

    /// <summary>
    /// SharkFinSubscribe
    /// </summary>
    [Map("225")]
    SharkFinSubscribe,

    /// <summary>
    /// SharkFinCollection
    /// </summary>
    [Map("226")]
    SharkFinCollection,

    /// <summary>
    /// SharkFinProfit
    /// </summary>
    [Map("227")]
    SharkFinProfit,

    /// <summary>
    /// SharkFinRefund
    /// </summary>
    [Map("228")]
    SharkFinRefund,

    /// <summary>
    /// Airdrop
    /// </summary>
    [Map("229")]
    Airdrop,

    /// <summary>
    /// TokenMigrationCompleted
    /// </summary>
    [Map("230")]
    TokenMigrationCompleted,

    /// <summary>
    /// SubsidizedInterestReceived
    /// </summary>
    [Map("232")]
    SubsidizedInterestReceived,

    /// <summary>
    /// BrokerRebateCompensation
    /// </summary>
    [Map("233")]
    BrokerRebateCompensation,

    /// <summary>
    /// TransferOutTradingSubAccount
    /// </summary>
    [Map("284")]
    TransferOutTradingSubAccount,

    /// <summary>
    /// TransferInTradingSubAccount
    /// </summary>
    [Map("285")]
    TransferInTradingSubAccount,

    /// <summary>
    /// TransferOutCustodyFundingAccount
    /// </summary>
    [Map("286")]
    TransferOutCustodyFundingAccount,

    /// <summary>
    /// TransferInCustodyFundingAccount
    /// </summary>
    [Map("287")]
    TransferInCustodyFundingAccount,

    /// <summary>
    /// CustodyFundDelegation
    /// </summary>
    [Map("288")]
    CustodyFundDelegation,

    /// <summary>
    /// CustodyFundUndelegation
    /// </summary>
    [Map("289")]
    CustodyFundUndelegation,
}