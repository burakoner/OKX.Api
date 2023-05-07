namespace OKX.Api.Enums;

public enum OkxFundingBillType
{
    [Label("1")]
    Deposit,

    [Label("2")]
    Withdrawal,

    [Label("13")]
    CanceledWithdrawal,

    [Label("18")]
    TransferToFutures,

    [Label("19")]
    TransferFromFutures,

    [Label("20")]
    TransferToSubAccount,

    [Label("21")]
    TransferFromSubAccount,

    [Label("22")]
    TransferOutFromSubToMasterAccount,

    [Label("23")]
    TransferInFromMasterToSubAccount,

    [Label("28")]
    ManuallyClaimedAirdrop,

    [Label("33")]
    TransferToMargin,

    [Label("34")]
    TransferFromMargin,

    [Label("37")]
    TransferToSpot,

    [Label("38")]
    TransferFromSpot,

    [Label("41")]
    TradingFeesSettledByLoyaltyPoints,

    [Label("42")]
    LoyaltyPointsPurchase,

    [Label("47")]
    SystemReversal,

    [Label("48")]
    EventReward,

    [Label("49")]
    EventGiveaway,

    [Label("50")]
    ReceivedFromAppointments,

    [Label("51")]
    DeductedFromAppointments,

    [Label("52")]
    RedPacketSent,

    [Label("53")]
    RedPacketSnatched,

    [Label("54")]
    RedPacketRefunded,

    [Label("55")]
    TransferToPerpetual,

    [Label("56")]
    TransferFromPerpetual,

    [Label("59")]
    TransferFromHedgingAccount,

    [Label("60")]
    TransferToHedgingAccount,

    [Label("61")]
    Conversion,

    [Label("63")]
    TransferToOptions,

    [Label("62")]
    TransferFromOptions,

    [Label("68")]
    ClaimRebateCard,

    [Label("69")]
    DistributeRebateCard,

    [Label("72")]
    TokenReceived,

    [Label("73")]
    TokenGivenAway,

    [Label("74")]
    TokenRefunded,

    [Label("75")]
    SubscriptionToSavings,

    [Label("76")]
    RedemptionToSavings,

    [Label("77")]
    Distribute,

    [Label("78")]
    LockUp,

    [Label("79")]
    NodeVoting,

    [Label("80")]
    Staking80,

    [Label("81")]
    VoteRedemption,

    [Label("82")]
    StakingRedemption82,

    [Label("83")]
    StakingYield,

    [Label("84")]
    ViolationFee,

    [Label("85")]
    PowMiningYield,

    [Label("86")]
    CloudMiningPay,

    [Label("87")]
    CloudMiningYield,

    [Label("88")]
    Subsidy,

    [Label("89")]
    Staking89,

    [Label("90")]
    StakingSubscription,

    [Label("91")]
    StakingRedemption91,

    [Label("92")]
    AddCollateral,

    [Label("93")]
    RedeemCollateral,

    [Label("94")]
    Investment,

    [Label("95")]
    BorrowerBorrows,

    [Label("96")]
    PrincipalTransferredIn,

    [Label("97")]
    BorrowerTransferredLoanOut,

    [Label("98")]
    BorrowerTransferredInterestOut,

    [Label("99")]
    InvestorTransferredInterestIn,

    [Label("102")]
    PrepaymentPenaltyTransferredIn,

    [Label("103")]
    PrepaymentPenaltyTransferredOut,

    [Label("104")]
    MortgageFeeTransferredIn,

    [Label("105")]
    MortgageFeeTransferredOut,

    [Label("106")]
    OverdueFeeTransferredIn,

    [Label("107")]
    OverdueFeeTransferredOut,

    [Label("108")]
    OverdueInterestTransferredOut,

    [Label("109")]
    OverdueInterestTransferredIn,

    [Label("110")]
    CollateralForClosedPositionTransferredIn,

    [Label("111")]
    CollateralForClosedPositionTransferredOut,

    [Label("112")]
    CollateralForLiquidationTransferredIn,

    [Label("113")]
    CollateralForLiquidationTransferredOut,

    [Label("114")]
    InsuranceFundTransferredIn,

    [Label("115")]
    InsuranceFundTransferredOut,

    [Label("116")]
    PlaceAnOrder,

    [Label("117")]
    FulfillAnOrder,

    [Label("118")]
    CancelAnOrder,

    [Label("119")]
    MerchantsUnlockDeposit,

    [Label("120")]
    MerchantsAddDeposit,

    [Label("121")]
    FiatgatewayPlaceAnOrder,

    [Label("122")]
    FiatgatewayCancelAnOrder,

    [Label("123")]
    FiatgatewayFulfillAnOrder,

    [Label("124")]
    JumpstartUnlocking,

    [Label("125")]
    ManualDeposit,

    [Label("126")]
    InterestDeposit,

    [Label("127")]
    InvestmentFeeTransferredIn,

    [Label("128")]
    InvestmentFeeTransferredOut,

    [Label("129")]
    RewardsTransferredIn,

    [Label("130")]
    TransferredFromUnifiedAccount,

    [Label("131")]
    TransferredToUnifiedAccount,
    [Label("132")]
    FrozenByCustomerService,

    [Label("133")]
    UnfrozenByCustomerService,

    [Label("134")]
    TransferredByCustomerService,

    [Label("135")]
    CrossChainExchange,

    [Label("136")]
    Convert,

    [Label("137")]
    Eth20Subscription,

    [Label("138")]
    Eth20Swapping,

    [Label("139")]
    Eth20Earnings,

    [Label("143")]
    SystemReverse,

    [Label("144")]
    TransferOutOfUnifiedAccountReserve,

    [Label("145")]
    RewardExpired,

    [Label("146")]
    CustomerFeedback,

    [Label("147")]
    VestedSushiRewards,

    [Label("150")]
    AffiliateCommission,

    [Label("151")]
    ReferralReward,

    [Label("152")]
    BrokerReward,

    [Label("153")]
    JoinerReward,

    [Label("154")]
    MysteryBoxReward,

    [Label("155")]
    RewardsWithdraw,

    [Label("156")]
    FeeRebate156,

    [Label("157")]
    CollectedCrypto,

    [Label("160")]
    DualInvestmentSubscribe,

    [Label("161")]
    DualInvestmentCollection,

    [Label("162")]
    DualInvestmentProfit,

    [Label("163")]
    DualInvestmentRefund,

    [Label("169")]
    NewYearRewards2022,

    [Label("172")]
    SubAffiliateCommission,

    [Label("173")]
    FeeRebate173,

    [Label("174")]
    Pay,

    [Label("175")]
    LockedCollateral,

    [Label("176")]
    Loan,

    [Label("177")]
    AddedCollateral,

    [Label("178")]
    ReturnedCollateral,

    [Label("179")]
    Repayment,

    [Label("180")]
    UnlockedCollateral,

    [Label("181")]
    AirdropPayment,

    [Label("182")]
    FeedbackBounty,

    [Label("183")]
    InviteFriendsRewards,

    [Label("184")]
    DivideTheRewardPool,

    [Label("185")]
    BrokerConvertReward,

    [Label("186")]
    Freeeth,

    [Label("187")]
    ConvertTransfer,

    [Label("188")]
    SlotAuctionConversion,

    [Label("189")]
    MysteryBoxBonus,

    [Label("193")]
    CardPaymentBuy,

    [Label("195")]
    UntradableAssetWithdrawal,

    [Label("196")]
    UntradableAssetWithdrawalRevoked,

    [Label("197")]
    UntradableAssetDeposit,

    [Label("198")]
    UntradableAssetCollectionReduce,

    [Label("199")]
    UntradableAssetCollectionIncrease,

    [Label("200")]
    Buy,

    [Label("202")]
    PriceLockSubscribe,

    [Label("203")]
    PriceLockCollection,

    [Label("204")]
    PriceLockProfit,

    [Label("205")]
    PriceLockRefund,

    [Label("207")]
    DualInvestmentLiteSubscribe,

    [Label("208")]
    DualInvestmentLiteCollection,

    [Label("209")]
    DualInvestmentLiteProfit,

    [Label("210")]
    DualInvestmentLiteRefund,

    [Label("211")]
    WinCryptoWithSatoshi,

    [Label("212")]
    MultiCollateralLoanCollateralLocked,

    [Label("213")]
    CollateralTransferedOutFromUserAccount,

    [Label("214")]
    CollateralReturnedToUsers,

    [Label("215")]
    MultiCollateralLoanCollateralReleased,

    [Label("216")]
    LoanTransferredToUserAccount,

    [Label("217")]
    MultiCollateralLoanBorrowed,

    [Label("218")]
    MultiCollateralLoanRepaid,

    [Label("219")]
    RepaymentTransferredFromUserAccount,

    [Label("220")]
    DelistedCrypto,

    [Label("221")]
    BlockchainWithdrawalFee,

    [Label("222")]
    WithdrawalFeeRefund,

    [Label("223")]
    ProfitShare,

    [Label("224")]
    ServiceFee,

    [Label("225")]
    SharkFinSubscribe,

    [Label("226")]
    SharkFinCollection,

    [Label("227")]
    SharkFinProfit,

    [Label("228")]
    SharkFinRefund,

    [Label("229")]
    Airdrop,

    [Label("230")]
    TokenMigrationCompleted,

    [Label("232")]
    SubsidizedInterestReceived,

    [Label("233")]
    BrokerRebateCompensation,

    [Label("284")]
    TransferOutTradingSubAccount,

    [Label("285")]
    TransferInTradingSubAccount,

    [Label("286")]
    TransferOutCustodyFundingAccount,

    [Label("287")]
    TransferInCustodyFundingAccount,

    [Label("288")]
    CustodyFundDelegation,

    [Label("289")]
    CustodyFundUndelegation,
}