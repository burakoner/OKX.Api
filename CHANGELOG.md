## Change Log & Release Notes

* Version 5.4.1217 - 17 Dec 2024
  * Fixed issue [#72](https://github.com/burakoner/OKX.Api/issues/72)
  * Synced models with the latest API documentation (2024-12-16).
  * Edited `OkxFundingCurrency` model: Added some new properties and marked some properties as obsolete.
  * Edited `OkxFundingNonTradableAssetBalance` model: Added some new properties.
  * Edited `OkxSpreadOrderType` enum: Added MarketOrder option.
  * Edited `OkxSpreadOrderCancelSource` enum: Added new options.
  * Edited `OkxAccountBillType` enum: Added new options and marked some options as obsolete.
  * Edited `OkxAccountBillSubType` enum: Added new options and marked some options as obsolete.
  * Edited `OkxAccountFixedLoanBorrowLimitDetails` model: Added new properties.
  * Edited `OkxAccountRestClient.GetFixedLoanBorrowingOrdersAsync()` method: Added new parameter.
  * Edited `OkxAccountFixedLoanBorrowingOrderDetails` model: Renamed some parameters.
  * Edited `OkxSubAccountType` enum: Renamed existing options and added new options.
  * Added `OkxTradeEasyConvertSource` enum.
  * Edited `OkxTradeRestClient.GetEasyConvertCurrenciesAsync()` method: Added new parameter.
  * Edited `OkxTradeRestClient.PlaceEasyConvertOrderAsync()` method: Added new parameter.
  * Edited `OkxPublicInstrument` model: Added some new properties.
  * Edited `OkxAccountBalanceDetails` model: Added some new properties.
  * Edited `OkxFinancialOnChainEarnOffer` model: Added some new properties.
  * Edited `OkxFinancialOnChainEarnEarningData` model: Changes data types for some new properties.
  * Edited `OkxFinancialOnChainEarnOrder` model: Added some new properties.
  * Added `OkxFinancialOnChainEarnFastRedemptionData` model.
  * Edited `OkxAccountRestClient.GetMaximumLoanAmountAsync()` method: Added new parameter.
  * Edited `OkxAlgoOrderType` enum: Added ChaseOrder option.
  * Added ChaseOrder functionality to `OkxAlgoRestClient.PlaceAlgoOrderAsync()` method.
  * Added `OkxAlgoChaseType` enum.
  * Edited `OkxAlgoOrder` model: Added some new properties.
  * Edited `OkxBlockRestClient.GetTradesAsync()` method: Removed state parameter and added isSuccessful parameter.
  * Edited `OkxBlockLegRequest` model: Added some new properties.
  * Edited `OkxSpreadTradeLeg` model: Added some new properties.
  
* Version 5.4.1202 - 12 Dec 2024
  * Merged pull request [#71](https://github.com/burakoner/OKX.Api/pull/71)

* Version 5.4.1025 - 25 Oct 2024
  * ApiSharp 3.1.0 Update
  * Fixed issue [#67](https://github.com/burakoner/OKX.Api/issues/67)

* Version 5.4.1021 - 21 Oct 2024
  * Renamed OKXWebSocketApiClient to OkxWebSocketApiClient

* Version 5.4.1018 - 18 Oct 2024
  * ApiSharp 3.0.3 Update

* Version 5.4.1011 - 10 Oct 2024
  * ApiSharp 3.0.2 Update

* * Version 5.4.1010 - 09 Oct 2024
  * Updated WebSocket API endpoints and models to [2024-10-04](https://www.okx.com/docs-v5/log_en/#2024-10-04) version
  * Moved Status methods to Public Client
  * Moved Announcement methods to Public Client
  * Removed Market alias for Public Client
  * Added WS / Fills channel
  * Refactoring and performance improvements
  
* Version 5.4.1009 - 09 Oct 2024
  * Updated Rest API endpoints and models to [2024-10-04](https://www.okx.com/docs-v5/log_en/#2024-10-04) version
  
* Version 5.4.1001 - 01 Oct 2024
  * ApiSharp 3.0.0 Update
  * Removed Json Converters
  * DTO models converted to record
  * Defined integer values for enums
  * Enabled nullable option for library
  * <ins>__Rearrenged namespace structure. Removed subfolder names from namespaces.__</ins>
  * <ins>__Removed obsolete/deprecated fields from both request and reponses.__</ins>
  * <ins>__Renamed: OKX.Api.Public.* -> OKX.Api.Public.OkxPublic*__</ins>
  * <ins>__Renamed: Trading -> Trade (both in Rest and WS Api)__</ins>
  * Performance improvements and optimizations
  * Fixed minor bugs
  * Commit: [d3705fb](https://github.com/burakoner/OKX.Api/commit/d3705fb3023c3c2c3bfd5e8c91cd26a78de4698f)
  * Commit: [0fdb84e](https://github.com/burakoner/OKX.Api/commit/0fdb84eb92f1b3bdedbb6169a0f5c36ff769294c)
  * Commit: [b5ff5f4](https://github.com/burakoner/OKX.Api/commit/b5ff5f4ba8ae64c5b664edaf36609a6257dc8391)
  * Commit: [de46c57](https://github.com/burakoner/OKX.Api/commit/de46c576c293909c7dcadb11c6e83ddaa5546c38)
  * Commit: [430b980](https://github.com/burakoner/OKX.Api/commit/430b980be71a6b8667c4e1235b6308146467b077)
  * Commit: [0e61bd7](https://github.com/burakoner/OKX.Api/commit/0e61bd7970a1e2c260640a8060e35c50a179e985)
  * Commit: [4319eef](https://github.com/burakoner/OKX.Api/commit/4319eefca22acde41ecdc6ae82a461836c457a60)
  * Commit: [2ca2f59](https://github.com/burakoner/OKX.Api/commit/2ca2f594ee62a830c0ae981f32f1f89b3442e800)
  * Commit: [bcd620b](https://github.com/burakoner/OKX.Api/commit/bcd620bf2e9ca3dcd00a52497b3a2a774651c7f8)
  * Commit: [b13746e](https://github.com/burakoner/OKX.Api/commit/b13746e021216f3be4ab80771852ff0b828309fd)
  * Commit: [577dc43](https://github.com/burakoner/OKX.Api/commit/577dc43dc82b0c4ce237277e23a0697136fddb2d)
  * Commit: [d7ed107](https://github.com/burakoner/OKX.Api/commit/d7ed10781067ca8b10f7de6cb7c9ebe11db78fa0)
  * Commit: [0e6f39e](https://github.com/burakoner/OKX.Api/commit/0e6f39e60fdeb52184e80644170c110a8d66411d)
  * Commit: [d95497a](https://github.com/burakoner/OKX.Api/commit/d95497a2f589ba328b574e8ed8457155f4432097)
  * Commit: [59ad075](https://github.com/burakoner/OKX.Api/commit/59ad075fb0bf97d880a9bd56f57669a30932f650)
  * Commit: [71c4e19](https://github.com/burakoner/OKX.Api/commit/71c4e195fde7d75685e7278897f63768774204ff)
  * Commit: [0f606cc](https://github.com/burakoner/OKX.Api/commit/0f606ccaf266e8c9e5b85ebd0748119870c7edf7)
  * Commit: [4e3aa5c](https://github.com/burakoner/OKX.Api/commit/4e3aa5c31e8478a3bf99fae0b8699393199de617)
  * Commit: [a546927](https://github.com/burakoner/OKX.Api/commit/a5469272ec3c2df9a273a96a54d899498cbfb1a1)
  * Commit: [259ec90](https://github.com/burakoner/OKX.Api/commit/259ec90bc4dae7248e10ed88f97cffdb721d9dea)
  * Commit: [02de742](https://github.com/burakoner/OKX.Api/commit/02de742510dc17b75cc46789e2c2d34c940d840f)
  * Commit: [85f1d02](https://github.com/burakoner/OKX.Api/commit/85f1d021fdc75f5a08541171a53145758f8e42f6)
  * Commit: [4d65f08](https://github.com/burakoner/OKX.Api/commit/4d65f080beeaf3760f461d5f4c55c1ce158b49b1)
  * Commit: [ae1a594](https://github.com/burakoner/OKX.Api/commit/ae1a594f70d5fa1eb820e5e76850aa820ed7a21a)
  * Commit: [81abffa](https://github.com/burakoner/OKX.Api/commit/81abffab473ed88222b548f7c9bbe4a2233c0cb2)
  * Commit: [ad0ae6c](https://github.com/burakoner/OKX.Api/commit/ad0ae6c15c2373fdee30f4cf2b629795604ad4b5)
  * Commit: [76fb7d2](https://github.com/burakoner/OKX.Api/commit/76fb7d26380da1c2780eff5aa236b0cbc592212a)
  * Commit: [5b1b8c2](https://github.com/burakoner/OKX.Api/commit/5b1b8c2bfd88ae6eff6aabf558dd2fc581f5b6d6)
  * Commit: [c497a19](https://github.com/burakoner/OKX.Api/commit/c497a198280025fc78f5e228d9a45869237d4f38)
  * Commit: [b992d78](https://github.com/burakoner/OKX.Api/commit/b992d787b43642508b557fc1024be03c01461859)
  * Commit: [6010e40](https://github.com/burakoner/OKX.Api/commit/6010e4096cb30c5a6f952efb1c14b4232ea4bdc2)
  * Commit: [07eba41](https://github.com/burakoner/OKX.Api/commit/07eba41be7b7f260dbc405521f91eae25242e605)
  * Commit: [5e4a249](https://github.com/burakoner/OKX.Api/commit/5e4a249ddf1c5212677536fbc4075eef57220bec)
  * Commit: [2ae7e74](https://github.com/burakoner/OKX.Api/commit/2ae7e74a9f3a0272816c397066d460d8a98f7b94)
  * Commit: [8dbc9ee](https://github.com/burakoner/OKX.Api/commit/8dbc9ee412efffaf2b96e7e1286fcf08b60f16ab)
  * Commit: [a54fb42](https://github.com/burakoner/OKX.Api/commit/a54fb42d7491c95345ec62ea77b0e78687f214a0)
  * Commit: [74a724d](https://github.com/burakoner/OKX.Api/commit/74a724d8b5663a67196b04a3ef0b91dfaf7093bf)
  * Commit: [5512722](https://github.com/burakoner/OKX.Api/commit/55127229161d8aa57907c4af0a4446432e143ecc)
  * Commit: [1673706](https://github.com/burakoner/OKX.Api/commit/167370629d3e2c00826407b7b4f4873c226be68c)
  * Commit: [34b7680](https://github.com/burakoner/OKX.Api/commit/34b768027834281fe2d72292527c79b231fc92eb)
  * Commit: [5415adb](https://github.com/burakoner/OKX.Api/commit/5415adbc29752bf0143b92424ff38d674bbc400e)

* Version 2.6.8 - 26 Sep 2024
  * Fixed Issue: [https://github.com/burakoner/OKX.Api/issues/66](https://github.com/burakoner/OKX.Api/issues/66)
  * Fixed Issue: [https://github.com/burakoner/OKX.Api/issues/65](https://github.com/burakoner/OKX.Api/issues/65)
  * Commit: [e5d1c52](https://github.com/burakoner/OKX.Api/commit/e5d1c52e13d16a4cad5e994bbae926e7249e2b1f)
  * Commit: [bb58152](https://github.com/burakoner/OKX.Api/commit/bb581525d1a341f47db55bc4bd4131b430a130a2)
  * Commit: [77ab68e](https://github.com/burakoner/OKX.Api/commit/77ab68e280089f7e6cbad3cfe309f3e225d44e32)

* Version 2.6.7 - 20 Sep 2024
  * Commit: [2452b07](https://github.com/burakoner/OKX.Api/commit/2452b0783f9120d0982a66e4a717c1844869ad8a)
  * Commit: [ba9fc7f](https://github.com/burakoner/OKX.Api/commit/ba9fc7fd57f5358635c91f325837624555524afe)

* Version 2.6.7 - 20 Sep 2024
  * Commit: [2452b07](https://github.com/burakoner/OKX.Api/commit/2452b0783f9120d0982a66e4a717c1844869ad8a)
  * Commit: [ba9fc7f](https://github.com/burakoner/OKX.Api/commit/ba9fc7fd57f5358635c91f325837624555524afe)

* Version 2.6.6 - 20 Sep 2024
  * Commit: [fba1657](https://github.com/burakoner/OKX.Api/commit/fba165764750442a260515826a8b18ac8827bd23)
  * Commit: [7653706](https://github.com/burakoner/OKX.Api/commit/76537063f3b658ff060ef7af24ded151ec076478)
  * Commit: [bde519c](https://github.com/burakoner/OKX.Api/commit/bde519c0d80099277691852f1b31d0a76303147a)
  * Commit: [aebfef3](https://github.com/burakoner/OKX.Api/commit/aebfef39fa2decd9a8f58521b258cf39d69cfdcf)

* Version 2.6.5 - 20 Sep 2024
  * Commit: [8e359e0](https://github.com/burakoner/OKX.Api/commit/8e359e05ce1b3640e1ab926149c1b667c522d061)
  * Commit: [ffecfa5](https://github.com/burakoner/OKX.Api/commit/ffecfa5a0fbb5b89f057cdaf39907b1faa6f4b87)

* Version 2.6.4 - 19 Sep 2024
  * Commit: [8ea9529](https://github.com/burakoner/OKX.Api/commit/8ea95299bef6e4c9438ef9112ab8dc8d4e89bd04)

* Version 2.6.3 - 19 Sep 2024
  * Commit: [6404572](https://github.com/burakoner/OKX.Api/commit/64045721c8bae673c06e180ed5ffdeabf8d71823)
  * Commit: [916966d](https://github.com/burakoner/OKX.Api/commit/916966d8588718a8a02214cbbf3765472206d68f)
  * Commit: [d55afc9](https://github.com/burakoner/OKX.Api/commit/d55afc99fe9f385847c5dca32305502c67a84544)
  * Commit: [b1e4e69](https://github.com/burakoner/OKX.Api/commit/b1e4e69e553e85bdf4e14a383d470355c0a34b84)

* Version 2.6.2 - 19 Sep 2024
  * Commit: [60f192e](https://github.com/burakoner/OKX.Api/commit/60f192e6c2ff803a037528103a46c4790ab6b2b0)
  * Commit: [5303b99](https://github.com/burakoner/OKX.Api/commit/5303b991b0598da4fe1590e3b1d99de31a0b5b69)
  * Commit: [4a7f2c2](https://github.com/burakoner/OKX.Api/commit/4a7f2c2631c63eb34818cfb19950c4c5b42caded)

* Version 2.6.1 - 19 Sep 2024
  * Commit: [6fb54f4](https://github.com/burakoner/OKX.Api/commit/6fb54f412894b5f891de6821f5926c43e71a8ca7)
  * Commit: [a722cdb](https://github.com/burakoner/OKX.Api/commit/a722cdbeb18e1d887498b3fa5ccbd11009860870)

* Version 2.6.0 - 19 Sep 2024
  * Commit: [01fd750](https://github.com/burakoner/OKX.Api/commit/01fd750c4702256e17822a6b3332c4d30454a8ad)

* Version 2.5.9 - 19 Sep 2024
  * Commit: [1546232](https://github.com/burakoner/OKX.Api/commit/1546232e9ac31d59f9970d70f7a952fa131f1d82)

* Version 2.5.8 - 19 Sep 2024
  * Commit: [162d76c](https://github.com/burakoner/OKX.Api/commit/162d76cce63256e066a85b9ab52ff860d7ea8dd8)
  * Commit: [7ecfd67](https://github.com/burakoner/OKX.Api/commit/7ecfd67356ad53af07c5b3b1b7d1389530bbeee3)
  * Commit: [75ddb1f](https://github.com/burakoner/OKX.Api/commit/75ddb1ffc1c28135304a7f640e1afd2d6ef4082a)
  * Commit: [56f331f](https://github.com/burakoner/OKX.Api/commit/56f331fb15fcc7e27e35c39e9206ec24126cd02b)
  * Commit: [1d1995e](https://github.com/burakoner/OKX.Api/commit/1d1995e932d155139e30a789ef1a24f7b415aa01)
  * Commit: [f15ed36](https://github.com/burakoner/OKX.Api/commit/f15ed36f454517d39c35140e9f6b1e66045369df)
  * Commit: [fa57592](https://github.com/burakoner/OKX.Api/commit/fa5759260cbee704fac32911eb6a4ed0bbc9a497)
  * Commit: [9dfbb59](https://github.com/burakoner/OKX.Api/commit/9dfbb59df7fa1df36721d0199fc05e0405e6813f)
  * Commit: [82ab429](https://github.com/burakoner/OKX.Api/commit/82ab4295ae31e8d0fd0fe85bf861fba895d7a816)
  * Commit: [33ba59e](https://github.com/burakoner/OKX.Api/commit/33ba59e475e9a096aa4902be2f6e391ec8b305d9)
  * Commit: [594dda1](https://github.com/burakoner/OKX.Api/commit/594dda12814759a7fbcd21514b9d5660113a9bc0)

* Version 2.5.7 - 18 Sep 2024
  * Commit: [aa93a49](https://github.com/burakoner/OKX.Api/commit/aa93a49c52cf208074280f4866c1b981b0d51115)

* Version 2.5.6 - 18 Sep 2024
  * Commit: [363887f](https://github.com/burakoner/OKX.Api/commit/363887f0e26f84ef4995b75aa73b38ed2251237d)
  * Commit: [ebd3a73](https://github.com/burakoner/OKX.Api/commit/ebd3a7313fb6e7003f0f97d1476383a812cd4d79)
  * Commit: [28ec4e4](https://github.com/burakoner/OKX.Api/commit/28ec4e4fc14c840f973247600329b088c1fbf99c)
  * Commit: [732d6c8](https://github.com/burakoner/OKX.Api/commit/732d6c830a37558a1fc9e7df220e2bc846c12381)
  * Commit: [a1a0a0c](https://github.com/burakoner/OKX.Api/commit/a1a0a0c9cc546df466cb6cbc0da19a8f90535567)
  * Commit: [64bfd20](https://github.com/burakoner/OKX.Api/commit/64bfd201d04feeb595ec70ca8d5d21a81387610c)
  * Commit: [b94edc7](https://github.com/burakoner/OKX.Api/commit/b94edc7ad7701a61115b021b95fa6b5536269b0f)
  * Commit: [8cc5044](https://github.com/burakoner/OKX.Api/commit/8cc504417b2b07ad94c8bbd4e7dcb59fd48a44e3)
  * Commit: [f2778fc](https://github.com/burakoner/OKX.Api/commit/f2778fcf3ba64bd53507600f557efff735857ea0)

* Version 2.5.5 - 18 Sep 2024
  * Commit: [5205ae9](https://github.com/burakoner/OKX.Api/commit/5205ae989f4e3dfff5e53f7a0bd3582feacaecea)
  * Commit: [6485367](https://github.com/burakoner/OKX.Api/commit/648536776afa9baba32b808822d1f7d85f3a4f15)
  * Commit: [069d5ea](https://github.com/burakoner/OKX.Api/commit/069d5ea2ac182c131538be277372907c3f4cf6c0)

* Version 2.5.4 - 18 Sep 2024
  * Commit: [0f4cc76](https://github.com/burakoner/OKX.Api/commit/0f4cc76eff8fbb3b6e6e403e2d515e2ec03cfc20)
  * Commit: [ff3c01d](https://github.com/burakoner/OKX.Api/commit/ff3c01d4fe23f2bf8bf92bd84c002b3f40ab3057)
  * Commit: [945f6ac](https://github.com/burakoner/OKX.Api/commit/945f6ac300fea6db048269604cca9898551e400f)
  * Commit: [8d0d2ba](https://github.com/burakoner/OKX.Api/commit/8d0d2ba3b4c5aa8a3f9c4b617f939fe8d1d1752f)
  * Commit: [c9636ec](https://github.com/burakoner/OKX.Api/commit/c9636ecae7db2e364428c1832bd9f5cc07e54fc4)
  * Commit: [c4674f0](https://github.com/burakoner/OKX.Api/commit/c4674f0f6e377473c0650ef92e7587774f356914)
  * Commit: [401d80e](https://github.com/burakoner/OKX.Api/commit/401d80e63daed677d12e0b6587bf9b6be54e97fb)
  * Commit: [e735e51](https://github.com/burakoner/OKX.Api/commit/e735e5158497660ea7dabf8d7761e96b879129cf)
  * Commit: [e6a5d0d](https://github.com/burakoner/OKX.Api/commit/e6a5d0d5641500d5d5ec14b2dc93c591a1989cad)
  * Commit: [3c5636a](https://github.com/burakoner/OKX.Api/commit/3c5636a930e4ab1bdf89ded948cf2ee1fcb4b3fe)
  * Commit: [541eadf](https://github.com/burakoner/OKX.Api/commit/541eadf2eef721484547cb208619eeb83f154954)

* Version 2.5.3 - 18 Sep 2024
  * Commit: [070bd1e](https://github.com/burakoner/OKX.Api/commit/070bd1e3aa14fccf5af067c9f0e06361ddba2930)

* Version 2.5.2 - 18 Sep 2024
  * Commit: [0bc6433](https://github.com/burakoner/OKX.Api/commit/0bc64338e0e4ca46814449199b44b8ac3357dc0a)
  * Commit: [c125158](https://github.com/burakoner/OKX.Api/commit/c1251584b7a07805b9013f3737c9250665518b58)
  * Commit: [c596810](https://github.com/burakoner/OKX.Api/commit/c5968101e14af72d72aa91289a4a388f5a678b1b)
  * Commit: [5059e46](https://github.com/burakoner/OKX.Api/commit/5059e464c11691227d8d8a525a569743f926001b)
  * Commit: [70e6353](https://github.com/burakoner/OKX.Api/commit/70e6353dde1629803708ef55ecbb43a5f3fda7e5)
  * Commit: [3b37e6f](https://github.com/burakoner/OKX.Api/commit/3b37e6fb22173fc0c1eafaf84547fc58ef231b9e)

* Version 2.5.1 - 17 Sep 2024
  * Commit: [eb7a84b](https://github.com/burakoner/OKX.Api/commit/eb7a84b47c8b9a8e54b060b0e9bf37527222bb65)

* Version 2.5.0 - 17 Sep 2024
  * Commit: [9d60b1a](https://github.com/burakoner/OKX.Api/commit/9d60b1a7846ad45e52a15bfbb67b4aa00258ca31)

* Version 2.4.8 - 19 Aug 2024
  * Commit: [bed2ae4](https://github.com/burakoner/OKX.Api/commit/bed2ae42ac3c542d12ffe4ae05e21fdf2c8386f9)

* Version 2.4.7 - 19 Aug 2024
  * Commit: [90d68aa](https://github.com/burakoner/OKX.Api/commit/90d68aabe240db5af1e9a6c219ecabb493193a57)

* Version 2.4.6 - 19 Aug 2024
  * Commit: [0377eb4](https://github.com/burakoner/OKX.Api/commit/0377eb48050b567cfb450071224398ad7a6bdd2a)

* Version 2.4.5 - 19 Aug 2024
  * Commit: [0a55896](https://github.com/burakoner/OKX.Api/commit/0a55896aab76af2c7843f9457542281f36f43106)

* Version 2.4.4 - 19 Aug 2024
  * Commit: [b60a1f6](https://github.com/burakoner/OKX.Api/commit/b60a1f6e98cea4db8dcac7819c51208f4406c974)

* Version 2.4.3 - 19 Aug 2024
  * Commit: [d391e5a](https://github.com/burakoner/OKX.Api/commit/d391e5ab2fd6f05d7ddd2b8413078790a29ae6ff)

* Version 2.4.2 - 19 Aug 2024
  * Commit: [4428798](https://github.com/burakoner/OKX.Api/commit/4428798a1a73635b4067befcbae3f4d395798785)

* Version 2.4.1 - 19 Aug 2024
  * Commit: [9bad8a8](https://github.com/burakoner/OKX.Api/commit/9bad8a8d4fd2264e8c2bbbc0602cad71bb081fde)

* Version 2.4.0 - 19 Aug 2024
  * Commit: [cbbd129](https://github.com/burakoner/OKX.Api/commit/cbbd129af2af144ea19330f0ed37083c6ba30d8c)

* Version 2.3.1 - 09 Aug 2024
  * Commit: [c0fe55e](https://github.com/burakoner/OKX.Api/commit/c0fe55e04377256b14d968170916fe98ac94b34e)

* Version 2.3.0 - 09 Aug 2024
  * Commit: [e0cf0d9](https://github.com/burakoner/OKX.Api/commit/e0cf0d91fecc17fa0abbc06bdebd04bcb774833c)

* Version 2.2.5 - 24 Jul 2024
  * Commit: [246fcb8](https://github.com/burakoner/OKX.Api/commit/246fcb8d1102bc57a876cf69f7c92e058880e3b0)

* Version 2.2.4 - 24 Jul 2024
  * Commit: [97aa599](https://github.com/burakoner/OKX.Api/commit/97aa5993ed9bb8364ab8635e70ae0c7dde7bc4ca)

* Version 2.2.3 - 24 Jul 2024
  * Commit: [3528e83](https://github.com/burakoner/OKX.Api/commit/3528e83ad9f9715deb71e8d7a0d4f821882ece37)

* Version 2.2.2 - 24 Jul 2024
  * Commit: [b22f1b9](https://github.com/burakoner/OKX.Api/commit/b22f1b9fa19e00d6e74889841152d1d72b723862)

* Version 2.2.1 - 24 Jul 2024
  * Commit: [a1bc300](https://github.com/burakoner/OKX.Api/commit/a1bc300ce95dd73cfdbe87c6ba73a757443585d6)

* Version 2.2.0 - 20 Jul 2024
  * Commit: [aca5c36](https://github.com/burakoner/OKX.Api/commit/aca5c36ae9e0778bd859508768d81d6b4b39fdcd)

* Version 2.1.8 - 20 Jul 2024
  * Commit: [6f1918f](https://github.com/burakoner/OKX.Api/commit/6f1918fb0695dc2d40633e5bdf905292550481b1)

* Version 2.1.7 - 20 Jul 2024
  * Commit: [01c0597](https://github.com/burakoner/OKX.Api/commit/01c0597d190e014e0b4108448e2f008cd6ad57c3)

* Version 2.1.6 - 20 Jul 2024
  * Commit: [606f892](https://github.com/burakoner/OKX.Api/commit/606f892cd8425703dd5c64b86315bb0aa5fae0b4)

* Version 2.1.5 - 20 Jul 2024
  * Commit: [d4e2ba2](https://github.com/burakoner/OKX.Api/commit/d4e2ba2d59e4a405568ddf31cdd42bb7a7746030)

* Version 2.1.4 - 20 Jul 2024
  * Commit: [e6d787f](https://github.com/burakoner/OKX.Api/commit/e6d787f97c995ae422f4a240ce3f0c5691eaffd1)

* Version 2.1.3 - 20 Jul 2024
  * Commit: [efb2af1](https://github.com/burakoner/OKX.Api/commit/efb2af1e32ca357481c545bfc5c23efbef73ffb2)

* Version 2.1.2 - 20 Jul 2024
  * Commit: [5a9cda5](https://github.com/burakoner/OKX.Api/commit/5a9cda50f65780aea96f46884a4732487d2b84b9)

* Version 2.1.1 - 20 Jul 2024
  * Commit: [56df8c8](https://github.com/burakoner/OKX.Api/commit/56df8c8a06aee146d746a36ea1aa10fc76ba9fbc)

* Version 2.1.0 - 18 Jul 2024
  * Commit: [9e2c48c](https://github.com/burakoner/OKX.Api/commit/9e2c48cf25207a7b086ddb10f3a03121de6840a7)

* Version 2.0.9 - 18 Jul 2024
  * Commit: [8d02eb4](https://github.com/burakoner/OKX.Api/commit/8d02eb4da4ad0cd7ffb7556e66e0eb30d421ffdf)

* Version 2.0.8 - 17 Jul 2024
  * Commit: [ac6d60e](https://github.com/burakoner/OKX.Api/commit/ac6d60e57f688f4b58f1eb32717745d9a0a12ef3)

* Version 2.0.7 - 17 Jul 2024
  * Commit: [5006d3b](https://github.com/burakoner/OKX.Api/commit/5006d3bdcd94ff908de8a88e35fca7edfc9e393e)

* Version 2.0.6 - 02 Jul 2024
  * Commit: [3dd122e](https://github.com/burakoner/OKX.Api/commit/3dd122e74008fb97b9c68d63bb6f635e82362831)
  * Merged Pull Request: [https://github.com/burakoner/OKX.Api/pull/61](https://github.com/burakoner/OKX.Api/pull/61)
  * Commit: [e313cfb](https://github.com/burakoner/OKX.Api/commit/e313cfb1ac0b8bfd5d760f448e42ba9c2b8ed7a2)
  * Commit: [8c72222](https://github.com/burakoner/OKX.Api/commit/8c7222281ef4d4af7ec97a52187df602cd2bb128)
  * Commit: [b9b48b1](https://github.com/burakoner/OKX.Api/commit/b9b48b1c0f1e5c87573ec8b3bda871a365534cd1)
  * Commit: [b09844d](https://github.com/burakoner/OKX.Api/commit/b09844dcfd22fea17a13d523f55bac265e8f5f5f)
  * Commit: [6fc07e0](https://github.com/burakoner/OKX.Api/commit/6fc07e06670e7ca1ba842f2947ee72bb9997325a)

* Version 2.0.3 - 06 Mar 2024
  * Fixed inheritance issue [https://github.com/burakoner/OKX.Api/issues/60](https://github.com/burakoner/OKX.Api/issues/60)

* Version 2.0.2 - 28 Mar 2024
  * ApiSharp updated to 2.2.1
  * Added Recurring Buy Section

* Version 2.0.1 - 22 Mar 2024
  * Changed main structure, edited tons of models, refactored tons of codes
  * Changed IEnumerable return types to List
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/58](https://github.com/burakoner/OKX.Api/pull/58)
  * Updated dependencies
  * Updated README file. Please refer to code snippets for samples

* Version 1.7.2 - 19 Mar 2024
  * Merged MarketData and PublicData clients

* Version 1.7.0 - 29 Feb 2024
  * Added WebSocket order management methods
  * Removed deprecated fields from request models
  * Released [https://github.com/burakoner/OKX.Api/issues/12](https://github.com/burakoner/OKX.Api/issues/12)

* Version 1.6.0 - 05 Feb 2024
  * ApiSharp updated to 2.1.0
  * Added feature [https://github.com/burakoner/OKX.Api/issues/56](https://github.com/burakoner/OKX.Api/issues/56)
  * Imported pull request [https://github.com/burakoner/OKX.Api/pull/55](https://github.com/burakoner/OKX.Api/pull/55)

* Version 1.5.6 - 03 Jan 2024
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/53](https://github.com/burakoner/OKX.Api/issues/53)

* Version 1.5.5 - 10 Dec 2023
  * Updated ApiSharp to 2.0.5
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/52](https://github.com/burakoner/OKX.Api/issues/52)
  * Refactored OKXRestApiTradingAccountClient and related models

* Version 1.5.1 - 15 Nov 2023
  * Updated ApiSharp to 2.0.1

* Version 1.5.0 - 13 Nov 2023
  * Fixed DemoTradingService usage
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/39](https://github.com/burakoner/OKX.Api/issues/39)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/48](https://github.com/burakoner/OKX.Api/issues/48)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/49](https://github.com/burakoner/OKX.Api/issues/49)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/50](https://github.com/burakoner/OKX.Api/issues/50)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/51](https://github.com/burakoner/OKX.Api/issues/51)

* Version 1.4.0 - 16 Sep 2023
  * Added Business WebSocket endpoint and moved related methods
  * Fixed websocket endpoint division (public, private, business) related issues
  * Some models changed (OkxPositionHistory)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/37](https://github.com/burakoner/OKX.Api/issues/37)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/40](https://github.com/burakoner/OKX.Api/issues/40)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/43](https://github.com/burakoner/OKX.Api/issues/43)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/46](https://github.com/burakoner/OKX.Api/issues/46)

* Version 1.3.1 - 06 Aug 2023
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/20](https://github.com/burakoner/OKX.Api/issues/20)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/36](https://github.com/burakoner/OKX.Api/pull/36)

* Version 1.3.0 - 06 Aug 2023
  * ApiSharp version updated to 1.5.0
  * Both Rest and Websocket Api client hierarchies synced with OKX Api Documentation
  * OKXStreamClient renamed to OKXWebSocketApiClient and methods moved to seperate clients according to OKX Api Documentation
  * Some method and parameter names changed
  * Timestamp conversion algorithm changed. You can now reach both timestamp and time properties
  * Added Copy Trading Section
  * Added OrderBookTrading.AlgoTrading.AmendAlgoOrderAsync (api/v5/trade/amend-algos)
  * Added OrderBookTrading.AlgoTrading.GetAlgoOrderDetailsAsync (api/v5/trade/order-algo)
  * Moved some MarketData methods to PublicData section: GetIndexCandlesticksAsync, GetMarkPriceCandlesticksAsync, GetIndexTickersAsync, GetOracleAsync, GetIndexComponentsAsync
  * Moved some MarketData methods to BlockTrading section: GetBlockTickersAsync, GetBlockTickerAsync, GetBlockTradesAsync
  * Removed some Funding methods: GetSavingBalancesAsync, SavingPurchaseRedemptionAsync
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/20](https://github.com/burakoner/OKX.Api/issues/20)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/29](https://github.com/burakoner/OKX.Api/issues/29)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/34](https://github.com/burakoner/OKX.Api/issues/34)

* Version 1.2.4 - 05 Aug 2023
  * Multiple subscription to index candle instrument name issue solved
    as described at [https://github.com/burakoner/OKX.Api/issues/30](https://github.com/burakoner/OKX.Api/issues/30) and solved at [https://github.com/burakoner/OKX.Api/pull/31](https://github.com/burakoner/OKX.Api/pull/31)

* Version 1.2.3 - 03 Aug 2023
  * ApiSharp version updated to 1.4.1

* Version 1.2.2 - 28 Jul 2023
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/28](https://github.com/burakoner/OKX.Api/pull/28)

* Version 1.2.1 - 28 Jul 2023
  * Synced with OKX Api 2023-07-26 version
  * Added some other missing documentation symbols
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/25](https://github.com/burakoner/OKX.Api/pull/25)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/26](https://github.com/burakoner/OKX.Api/pull/26)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/27](https://github.com/burakoner/OKX.Api/pull/27)

* Version 1.2.0 - 27 Jul 2023
  * Added documentation symbols
  * Synced with OKX Api 2023-06-28 version
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/21](https://github.com/burakoner/OKX.Api/issues/21)
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/21](https://github.com/burakoner/OKX.Api/issues/21)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/23](https://github.com/burakoner/OKX.Api/pull/23)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/24](https://github.com/burakoner/OKX.Api/pull/24)

* Version 1.1.7 - 26 Jun 2023
  * It's possible to subscribe multiple symbols at once on WebSocket
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/16](https://github.com/burakoner/OKX.Api/issues/16)

* Version 1.1.6 - 26 Jun 2023
  * Updated All Methods and Models, synced with OKX Api 2023-06-20 version
  * OKXStreamClient has some parameter and parameter order changes
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/18](https://github.com/burakoner/OKX.Api/issues/18)
  * Fixed some typos

* Version 1.1.5 - 25 Jun 2023
  * Added Grid Trading section endpoints
  * ApiSharp updated to v1.3.6
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/11](https://github.com/burakoner/OKX.Api/issues/11)

* Version 1.1.0 - 07 May 2023
  * Updated All Methods and Models, synced with OKX Api 2023-04-27 version

* Version 1.0.6 - 06 May 2023
  * Updated WithdrawAsync Method [https://github.com/burakoner/OKEx.Net/issues/97](https://github.com/burakoner/OKEx.Net/issues/97)
  * Updated GetInstrumentsAsync Method [https://github.com/burakoner/OKX.Api/issues/7](https://github.com/burakoner/OKX.Api/issues/7)

* Version 1.0.0 - 25 Mar 2023
  * First Release
