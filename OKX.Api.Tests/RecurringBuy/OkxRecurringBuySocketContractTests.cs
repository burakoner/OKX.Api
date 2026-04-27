using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.RecurringBuy;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OKX.Api.Tests.RecurringBuy;

public class OkxRecurringBuySocketContractTests
{
    [Fact]
    public void OrderUpdatesSubscription_UsesBusinessSocketAndLogin()
    {
        var method = typeof(OkxRecurringBuySocketClient).GetMethod("CreateOrderUpdatesSubscription", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var result = method!.Invoke(null, [OkxInstrumentType.Spot, (long?)123]);
        Assert.NotNull(result);

        var tuple = (ITuple)result!;
        var endpoint = tuple[0];
        var authenticated = Assert.IsType<bool>(tuple[1]);
        var request = Assert.IsType<OkxSocketRequest>(tuple[2]);

        Assert.Equal("Business", endpoint?.ToString());
        Assert.True(authenticated);
        Assert.Equal(OkxSocketOperation.Subscribe, request.Operation);

        var argument = Assert.Single(request.Arguments);
        Assert.Equal("algo-recurring-buy", argument.Channel);
        Assert.Equal(OkxInstrumentType.Spot, argument.InstrumentType);
        Assert.Equal("123", argument.AlgoOrderId);
    }

    [Fact]
    public void OrderUpdatesSubscription_RejectsUnsupportedInstrumentTypes()
    {
        var method = typeof(OkxRecurringBuySocketClient).GetMethod("CreateOrderUpdatesSubscription", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var ex = Assert.Throws<TargetInvocationException>(() => method!.Invoke(null, [OkxInstrumentType.Swap, null]));
        Assert.IsType<ArgumentException>(ex.InnerException);
    }
}
