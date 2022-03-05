using System;
using System.IO;
using NUnit.Framework;
using UnlockToolShared.DataAccess;

namespace UnlockToolTest.DataAccess
{
    public class UnlockResponseReadDataAccessTest
    {
        private byte[] goodFile = Convert.FromBase64String(
            @"rPxpIUborWZxsJUNJwp+10RBzTCekrkazccasc71W5UZkbX7AWIrFcXOilVp0u3ol14qoXSsblvfn0jK1AeF+AknvGMDDr4wfD8zEPf8SYNhx1qD9xybmr4eOkS9hR9OQ/d6HOHwu1q9PyhxoO7plC+cxEac2fesbkDd1g08xraDp7roh+4iVcH15zjaqASmU9LmS3vq2jmAPWqRqJt78GPcwKU8JBfIX4hCY8CM3H5l+49yWEdeBqE5XoUV3epxkEot7albCPVrvDtYEvl01Oz6C4Y7MaDl7l1BNW8ot1F2YtL2bsB5/JAbD2TvIgPv2qgdUHCAF1pwxYC7JR3/YbYu16LRcFqeboCIlqtc3ZgfYR/K5Do8TpFHWP3+lG3PDDJmavbkvUF8AnSaASpGQWR+7QUyoy2dnthnDeY+bj+yzFxZou4VvVbwyVxognmloV7d0tAsFgwNSMyO6kjzX6vqNbjaZ4VMW0WhNWjMXh+s04eVlaSIVo3ffiC7gyCkLcUMltf/jxt5r2+j2reqmoE50SfU9G1YClgLKQkoBtbAUo0w2cNhhYS9YLu8OUBYhQWu0GppwzcwTTGANeUsK9w2p4KnP2QLzASEj9l36jY/RidhAxrNszYjArvt/oU5QGanZZbrEJoJmHace3NW8NhxxE70Y48/eANUPyfucCSGUTytgS4qWNaGC9ZN2RFfh8OudYm9DBR2bWyGEtABT+uAb105QE8zNwIFr+MMpNvQbjAQ+DkmLE9bzzFlcy/I5PKvQydYPwN5JTpqYUVWqpWm0/0ZCrL4GaGbGow1NG6QF5hNZLCNI/jaPDy+pGmtsy7G4Ez3ScHeTKp4FEZjr38ldhnNreQFpq9qmb4+zFy8SvOwpBujzRZUyDsbP73tGC0P6JYrL29RmkedfxFSTwoyDDCQYFU+ZkytEyZvfGllruCulIa86N2lyh+YTIHDGjH/f1cdhFNPnnKvtuVKqYlvh9mavov+/ipwLwuMjryqNZCJq3WaSMhl4HTo7fYvRD61GcpQYu7pS9sVigWrlF0iYIpC2gOmv0Z5m71dlxO43xplqfRsGcWcyv6TQ7Cg5uA8jb3cg1KGE8VisXikjSxlTNgeT4F3l1TFQipFK9lWrLrT4pAHyiATX8qh50ZjRg4nkg2fC95lAB/X+zZOgYb6L+M/2vIGxMRRAsL+RkGiQHlEw/ojKy5SWRJWUpjW8YRlstvVpfGuRcm62mKP/0ATN8KMi9f21g2ObCAoWIWI+IZUXXrGVr+z1mGk9wS4JxW1ZeyUfjEgFrw9aeR0yuKDagFBg6/iT0xZfQA7hSN8688NEPNQJhn6oBU1NScLQJFVCYVxpOjO+ofQts5cr3V4goZF26GFU5ffyP1dbDY="
        );

        private byte[] noLevel2HashCodeInFile = Convert.FromBase64String(
            @"rPxpIUborWZxsJUNJwp+10RBzTCekrkazccasc71W5UZkbX7AWIrFcXOilVp0u3ol14qoXSsblvfn0jK1AeF+AknvGMDDr4wfD8zEPf8SYNhx1qD9xybmr4eOkS9hR9OQ/d6HOHwu1q9PyhxoO7plC+cxEac2fesbkDd1g08xraDp7roh+4iVcH15zjaqASmU9LmS3vq2jmAPWqRqJt78GPcwKU8JBfIX4hCY8CM3H5l+49yWEdeBqE5XoUV3epxkEot7albCPVrvDtYEvl01Oz6C4Y7MaDl7l1BNW8ot1F2YtL2bsB5/JAbD2TvIgPv2qgdUHCAF1pwxYC7JR3/YbYu16LRcFqeboCIlqtc3ZgfYR/K5Do8TpFHWP3+lG3PDDJmavbkvUF8AnSaASpGQWR+7QUyoy2dnthnDeY+bj+yzFxZou4VvVbwyVxognmloV7d0tAsFgwNSMyO6kjzX6vqNbjaZ4VMW0WhNWjMXh+s04eVlaSIVo3ffiC7gyCkMwU5JYCs4revhdIOKGO5CXQDx6DWCbNTUH9LcOLOoLEE43ejDTtqcRK/kNK9CYOVjQJgiitoo2M6i3AmOBTemDl9btnE6/D/EjLAAr7w2k570nM9/JLVBh7r2/KmbCOjJ+n38HclQglTJPG61iwGpLIhzuDyfZvylz2mmyXjnoMyfYybdRtgZOhQnLf6K1Z3U4MEjCD+xbUWjjGyGPUda30mAy3j6fj7yTQjuO8xqPu+w2LQgEdIMxzqUHD55vXwWSoKA2Xe9BwRasONLtcGo0oWeH6rN+CgiRtxcKcZ+wgrgf+iO8DzF+JoZDQbENb8SaOGwG32UmIu6fvhzjHUPT404p31RxxZOX16Blqcpm12wF5h0ToAcKVQlXN7EpzKdeNRKT3y3geceE55x2vGpLKwWs/l+JYryWepjUOkTceh6/OWib6o7fEVLMYezaHa1f7UyUji95SloLrSH/gHmdOXXPNxVJT9y1rwMT2o9KWfd9y9xlnc4g2M96rVB1DoXZbzFIod2OIgl/hNgxkz7WERIehMkhoyq/0asdyqfRDkKqDJJTHziFhY8p/J29oPdxnxZfCL8bVBY1n+pPlJZ6NUp6lnKbRaY/pE/lxR3o8tIxBrKhvzvTj8NXr9pGvzgwj9XWkQkumn6S0FgRIbA5NIkuj+Mp6sQ+mY+jiySmZLSEn2DHGotqigFnHrFt9W"
        );

        private byte[] wrongLvl2HashCodeInFile = Convert.FromBase64String(
            @"rPxpIUborWZxsJUNJwp+10RBzTCekrkazccasc71W5UZkbX7AWIrFcXOilVp0u3oBbfvxJFYMZwCobP6tM6QLI4x0s+rdzcRKpQOKGOZ8H/zjW1QmnCKqDfZ1dBcxxo2XoPaQEKjlKqGPxC89v0lKlxst/0SbduNSxRzDlhI0FccXmwwDUWUHuSqYQwzYtumKlidiZ3P3Wh1eoDUO1rTflLT0hm5XvrbfLuo6bk/PqkISL4zurDlLb7f03F5MtGDjvVP38PaOSA2U/3fDTv9yaT6b9ZfG+JUJXGNJFUUidn7G9OTNIJHeYF8Emzq+q96EGwUKZ244AyR8aarcgAjxcfGd9rVB9J/3+fncROH5g8KdywRNMDRoc4xuL9dyo0eVK7iuv+JHYKFMbg958o/kvZpX+MQMw4iLYJpQ9vS8SkvayCTg0lOq5vcQJy9nILLrcTCtkIfSayvUX7JA9Vt2xUBi7bIboHqxShuZOt81h6sMHME3njTEZDEuLvh5u20FFHYIlsnnIT/DfOXmtmAbmNtYk9ULMeCk3jGhzoWhKFdTEeJrhbQa/i/qZShiu2m5Gygc83+91mUWw4PVKOtXORDP+BWC4YjZ0yKa9u5LGEAVtLBpRhDXKcF5b9y3Q7azfhi1yk3xoYR8XoPW/Sp4SROw9QKkslM1EMqUw+VRGaWh/uTDXqERcm8NGAVglz+kTgw0KIIxUv86astJ94Q76U/V27Vyrxz3PZFID60S6UIGVRBBDizZeys+hwe8bP3BcliujHVwxbE/2Uv+s2lk0ukuHIC/xpP3fR+DpnH+hPqA737YxY9QImjxuEnJmc+UGzG1JZLRy1PtjLZfLzhmjrPBTbs+nROaPDm3cV0SXjJMXb9vlnvLEerUYprOeEAcRfCsKyN21gPSKPsMfbkgRhIlYdioeKyV2+JLKvR6jF1pMNnc9Iqci8yMu4BfTbhsaadZ36d79efYxT1dctE/zwNBauW50S30DwCKmqrF62iQ1lSLLpSIv7BN0ur4Y/iNl16plCc2MQVJ0bY9R0ARaEUqZJWqbAnGq+82t3XEiHoUGfKytCjn0T89OR52WPA9cESkQUve5EdBid7VVPH5n5H86YMdcMVAJ0/b5ClBLLt1SDwRR/gC5ajZAXcoFQ32ZakXDJ3fI+0cT4UjuoEFCpHCNY/8HqGYuPE0YWaku2/Nak4Hrbt6zZhTkA8ysj0wsBd+as1SJq5UqXNkgCpJt/6XxhUEo2JE90fnqwUKIst98CLKlpD6NK9WKRJF+I+USvd0pXCxk+CJz1QlVznu5SFL7At8yV6KjAK7Yop0fqQVuvVCKpU7AE8NUavWQv+xtt8rtxuwETjgEbNv/Gpdrnec3UZ/YR6RpPVBhISyD8="
        );

        private byte[] noRsaSignatureInFile = Convert.FromBase64String(
        @"rPxpIUborWZxsJUNJwp+10RBzTCekrkazccasc71W5UZkbX7AWIrFcXOilVp0u3ol14qoXSsblvfn0jK1AeF+AknvGMDDr4wfD8zEPf8SYNhx1qD9xybmr4eOkS9hR9OQ/d6HOHwu1q9PyhxoO7plC+cxEac2fesbkDd1g08xraDp7roh+4iVcH15zjaqASmU9LmS3vq2jmAPWqRqJt78GPcwKU8JBfIX4hCY8CM3H5l+49yWEdeBqE5XoUV3epxkEot7albCPVrvDtYEvl01Oz6C4Y7MaDl7l1BNW8ot1F2YtL2bsB5/JAbD2TvIgPv2qgdUHCAF1pwxYC7JR3/YbYu16LRcFqeboCIlqtc3ZgfYR/K5Do8TpFHWP3+lG3PDDJmavbkvUF8AnSaASpGQWR+7QUyoy2dnthnDeY+bj+yzFxZou4VvVbwyVxognmloV7d0tAsFgwNSMyO6kjzX6vqNbjaZ4VMW0WhNWjMXh+s04eVlaSIVo3ffiC7gyCkLcUMltf/jxt5r2+j2reqmoE50SfU9G1YClgLKQkoBtbAUo0w2cNhhYS9YLu8OUBYhQWu0GppwzcwTTGANeUsK9w2p4KnP2QLzASEj9l36jY/RidhAxrNszYjArvt/oU5QGanZZbrEJoJmHace3NW8NhxxE70Y48/eANUPyfucCS9qsdyBEF1m76j/VOYKMpI"
            );

        private byte[] wrongRsaSignatureInFile = Convert.FromBase64String(
            @"rPxpIUborWZxsJUNJwp+10RBzTCekrkazccasc71W5UZkbX7AWIrFcXOilVp0u3ol14qoXSsblvfn0jK1AeF+AknvGMDDr4wfD8zEPf8SYNhx1qD9xybmr4eOkS9hR9OQ/d6HOHwu1q9PyhxoO7plC+cxEac2fesbkDd1g08xraDp7roh+4iVcH15zjaqASmU9LmS3vq2jmAPWqRqJt78GPcwKU8JBfIX4hCY8CM3H5l+49yWEdeBqE5XoUV3epxkEot7albCPVrvDtYEvl01Oz6C4Y7MaDl7l1BNW8ot1F2YtL2bsB5/JAbD2TvIgPv2qgdUHCAF1pwxYC7JR3/YbYu16LRcFqeboCIlqtc3ZgfYR/K5Do8TpFHWP3+lG3PDDJmavbkvUF8AnSaASpGQWR+7QUyoy2dnthnDeY+bj+yzFxZou4VvVbwyVxognmloV7d0tAsFgwNSMyO6kjzX6vqNbjaZ4VMW0WhNWjMXh+s04eVlaSIVo3ffiC7gyCkLcUMltf/jxt5r2+j2reqmoE50SfU9G1YClgLKQkoBtbAUo0w2cNhhYS9YLu8OUBYhQWu0GppwzcwTTGANeUsK9w2p4KnP2QLzASEj9l36jY/RidhAxrNszYjArvt/oU5QGanZZbrEJoJmHace3NW8NhxxE70Y48/eANUPyfucCSyk8e7Jywe3apA8k/ELfHgYCzxIqfmtpvxO5zlgHLY8xXNfESH3C+k6+1jjYmgUt4kWXITyH0L7tpheogwH8/ET3Kix/gl2b05gDEw3H/ZuwrqNof5TisBhZ9Rc67LJd8ssvLrchJbwSaFfT2jtM+Q2H4Vu+elTXDUpD1tWeBN2DiU8K+X72v/Kr4F6MjkJZF9ADYYJudzyLQUwtkWpj0KQfNAxW6aC5ORyqsikQPWz/YzPBxVCk1Ud9Xrtl2tu7ktR2lMUDzfO3Imt98/SQpD3VQCVlrmyC+cuGj/RZrQS4mV3i+w0FiDd4xl4lRrcJVrkGdWu4lhS2MjB2UgDt06Yg1rBv931MxF8HrJG99O5mW0FBhVL6iOL7GqKOgmFGhe9C/l+8TlimNNVXzDsKxNbO1AxbyAsaBw8xyZY/bYYIr7bGNez2zJIxVZ24TYIQi0pRBgjHpWcsv8MIlPXFqsq/VqCf9vsgx59opAtJDWqI3aLbbrH7ab/bSzi4K5EbOnBdjMVj+VcdDffTnvew8/Ww/cUu5o6pR8jMs9BC6CfpG6ozpY4AJ11vO33gk9NNa+Em9EwfsfT0FWobfA/hbZy+dP3Be5eWFgVc+vGX+fZ/sPevWbnB5DjAj7WWMkcw9jlNX4O8hxR+6HN0DkVSgcdpcwb/y4uxoC91im6pN39qHWMGsYQj9f7KGfHBhxzDY="
        );

        private byte[] lowHashAndLowFileSize = Convert.FromBase64String(
            @"yZ/OHrxQmr3UV+y6TzySiHaMSstPiLIJ4DALGtuM3dQ5RLws+x6LygzLz6LFRLluviN0CMu/d+RAPTmHDwrTOjHV3eLeOWYZlgCOcy3+GA/0Z87I5gSKXAxPQhDV+85NgFPbrYW8kDZ33Vv8DgLLuLgdd+JJf7HQPm7S32B1O9anGj2evz5WOfoEv0myEzWlhov8hTXR25C2OaFR0V++O7erUv4uAekO8i/kKR2oLixHk2M3gs2qew74uQQJuIdjDTsLi+Lf4g04epHfRN/lFJqVUjjmFD66YwUhW7PgP57msiVADrUypOvkEgADcM2gu4arGyYGwGSdd3/9aUj1o0ZHHJA42L2JURmF0KW1nwoXXz+5Jkn8e+c6VF2WFF1Mm4thuYcIIFEHTDiPMVirbrSAeS7ddiO0MWmvCO9mDXrVdl+JrM8n78ZFPUN1Mo+Vp4wq2Baf4kf/Feq26CI23Sj1JWBIKRVDJ/U/xgy+GWpxTUT4OxifIrCzL4RXuSgyX2No079OfvyQw4BYaJrcew/edL19/ho4AZMKjtpKZkMLMoGXKyhJAoyoDAnTozE7U7iEExh2BlpFUDjiHA6HBswR0Pc5ZAHhOrEV0B4eN5EDa0E+cH/OyqNfCnHIb29YU0YFZNpGlEZm3J0Uy80s3hvHWrLy/qjQkNNTYr3s92QaKxGOoX1K9zwLUamlez7OSCZeG3xvuObRRAzmCcLAJv7SvuHjilhqctPKLiIxCgwtniv86Qb01sb8OeJjoDjOfwsXYtOwSt4JqlVEoSjAuQ=="
        );

        private byte[] lowFileSize = Convert.FromBase64String(
                @"nALSV9aQz2OwgImkwxZisP6EirNUfdOtxGv4f+zYrGuBZgRdIku6g1UOekKvEnvnYM4qUQXZqjw6OQUB9n2gjRbJSQpKbz5a61jpxISguN9dJ4hSQtYVXvpejM3tKNFoNN+1GBn2ymd9JJQuqGU+nvXvd/Tzmu6Hbj6CLvT/1vLKwln1KLcAvOUoNC3QBDMoHFEhB5PoJv/0ya0CXkLi717SRVlA4uyDSntAavZ4yxL/WBshdNPq3dXv+g5I5AsmISeQs+zkZS8WgksfeYhHfisHaDmW2thRivDqnUWWJrD2jCc43OjLiKlRJjRX7uQJNqq3bWvx2S/3CzjHPIBkBSzjwBaTUwi+sAIbfsxtgMkNp45Y2iEw5t+Cu/wzYfgxLjDamS+suMLlkigcxdwEQA6b3xhHtv/B2LAUtU3k1PKMbimWOxYXU7HO/uMc5SkaP0FBP0sr0k1uG+cp6v9YT5/Lm+RkSEz1S8V2zgAg6HeiDPDtVU2EhwPIvAJV4M4TGXw145yu7G+ysf1dFWd7Yx3NzCYIb9+wsrQZUId0hXeF6YqWUNitD7C1POYIOJGhj6DOjudiNB01QiAOLu+nG+4dH6DFdeT0FTfQ++ff26qCg7io8Grdrlb1l61K+So7GrBsLWoQIOgv1iPsQdR6O8ZbUyHJ2sDItec7QtJ1vu++M3uctxWukNDnWfZW1v55BaBHJQMl+6DU+aC2BCmtWyCzs51S4/X+w91y85Y5SdsTzcCUBPMX1Nc0iVDjJxGUxPKPOYL+f86MDism13Osw/px5TmLVHMCnhp6rjuLUsWWMvGKM1BJ7u/sbMQaGKDTfi5IatghLO6GU40sCih/EV14Q1vUSQ7dG4IsJ1oT6h8="
            );

        private byte[] invalidJsonFile = Convert.FromBase64String(
            @"iKuZvB4+Loyp+DdQ5yz8PwhC4Z6zDIQIsK2M0ZWDkL7f23UJo8lJMJqW9L7S8mBX7U7KIO8lB5ms8mkrUvAcxYKZ9a9GdLXQbzZzB8qKG+POj9AJpVyPVW6Sg9rDfTapdcfnd0Xs6Kpc1BWNqT8+sJm8UHzcMkHDpS0ATQZz8vtF7KPNRFPnbVT45TxyjSK7ZqHw95ybEyJaTuFC2SMmPHIcGfjdlcCVQ3KMpILwzcHF6uPQRbFDjFGIDmRHnbyNNQ/lHZrxoLX6UanQq6ojkJNmV3epwudgw29FqxcWxyWOUuZgrCc9bN7ZToZKSqCc+srZe4LUINY0WzVn6b+XyC4oLvIpA+gKMdCQPAk/1zBTBKVvxnMjGwQrRO9ANkE/O0d6jYbTNsyr5iFTTx4BTF/67xbz+4oBwrnYpN2gQ6Ul9l/QPHbfo7lmRQIUS6t+6GWRwcsEE/FoFDfmA/MMYY9u80AvOmhxE2Q7Yec0MRUr8U4/ZeMGoPtGbhU/8mCh1NXR0o8urlz5OY3fd6GjZhawEwMK92M6LWFeuV2uqOqoMLArGII5T/CB0l4Y3dn2ki26/NuHHJvxo4No42A+sKjRbgKd6Z0xeD/5AkM1YNl1wPABl0brKpeGU8xVvelFauAQvrK/HvTWZ5vrbo1HHS+nf7zH6Z/2gMn08WFHfrAr6eqAywZAieB5xM4SJSPX9VyONw4DDQykJoQSjj/OgwAdAdeQka+L5FJhWsVMOVHmc+hHac7RsYwfFaTyxLNvtTmD7HWkIjqv/piSDE//gWe2Pjy9qV8s74MRBdD/kmkHlX/OjhtELJfXcfZw8jzEoe4+UEFPiJkYpkir6Ic6Gz8PRwmeZyv5ooUeJqhS8i9couRDDczrAds/qk62dblx"
            );    
        
        [Test]
        public void GetUnlockResponseFromWrongLvl2HashCodeFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(wrongLvl2HashCodeInFile);
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("SecondLevel Hash not Valid"));
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromGoodFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(goodFile);
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                var unlockResponse = da.ReadUnlockResponse(stream);
                Assert.IsNotNull(unlockResponse);
            }
        }

        [Test]
        public void GetUnlockRequestFromNoLvl2HashCodeFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(noLevel2HashCodeInFile);
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("SecondLevel Hash not Valid"));
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromWrongRsaSignatureInFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(wrongRsaSignatureInFile);
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("RSA Verify Failed"));
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromNoRsaSignatureInFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(noRsaSignatureInFile);
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (Exception)
                {
                    Assert.Pass();
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromEmptyFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (Exception)
                {
                    Assert.Pass();
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromLowHashAndEmptyFile()
        {
            using (var stream = new MemoryStream(lowHashAndLowFileSize))
            {
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("FileSize to low"));
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromEmptyFileButHash()
        {
            using (var stream = new MemoryStream(lowFileSize))
            {
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("FileSize to low"));
                }
            }
        }

        [Test]
        public void GetUnlockRequestFromEmptyInvlaidJsonFile()
        {
            using (var stream = new MemoryStream(invalidJsonFile))
            {
                stream.Position = 0;
                var da = new UnlockResponseReadDataAccess();
                try
                {
                    var unlockResponse = da.ReadUnlockResponse(stream);
                    Assert.Fail();
                }
                catch (InvalidOperationException e)
                {
                    Assert.IsTrue(e.Message.Contains("Error by Parsing Json"));
                }
            }
        }


    }
}


