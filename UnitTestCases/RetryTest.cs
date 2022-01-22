using RestSharp;

public class RetryTest
{
    public IRetryStrategy RetryStrategy { get; set; }

    public RetryTest()
    {
        //This can be depdency injected
        RetryStrategy = new CustomRetryStrategy();
    }

    public async Task VerifyRetry()
    {
        var exceptionType = typeof(HttpRequestException);
        var method = () => CallApi();
        var result = await RetryStrategy.RetryOnException<RestResponse>(method, 3, new List<Type>() { exceptionType });
        System.Console.WriteLine(result.Content);
    }

    private async Task<RestResponse> CallApi(params int[] args)
    {
        var random = new Random();
        var randomNumber = random.Next(5);
        try
        {
            if (randomNumber < 2)
            {
                //This is simulation for exception
                throw new HttpRequestException();
            }
            else
            {
                var client = new RestClient();
                var request = new RestRequest("https://wise.com/login/",Method.Get);
                request.AddHeader("authority", "wise.com");
                request.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"96\", \"Google Chrome\";v=\"96\"");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-user", "?1");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("referer", "https://wise.com/in/");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("cookie", "appToken=dad99d7d8e52c2c8aaf9fda788d8acdc; gid=b991a0de-58ab-4270-b6f6-3b5c9d06ecfe; __cf_bm=KLfsSdct5O0rCe1AY5T5XHfKTmSU7ZVC_pDGsyrWnjE-1641589633-0-AWXFdt8YOJ2T/w+eOcERJ9pyWThAbh6l1Z0cbcK3s9qDhkvEvUysVeFm2saxJmUTpqbUZbn+xsnsVx5BssXXKvthYm9BQISOdw+gW8abC0sP; twCookieConsent=%7B%22policyId%22%3A%222020-01-31%22%2C%22expiry%22%3A1657314434304%2C%22isEu%22%3Afalse%2C%22status%22%3A%22accepted%22%7D; twCookieConsentGTM=true; mp_e605c449bdf99389fa3ba674d4f5d919_mixpanel=%7B%22distinct_id%22%3A%20%2217e365df57ebf0-021a512083dcb1-4303066-1fa400-17e365df57ff2f%22%2C%22%24device_id%22%3A%20%2217e365df57ebf0-021a512083dcb1-4303066-1fa400-17e365df57ff2f%22%2C%22%24search_engine%22%3A%20%22google%22%2C%22%24initial_referrer%22%3A%20%22https%3A%2F%2Fwww.google.com%2F%22%2C%22%24initial_referring_domain%22%3A%20%22www.google.com%22%7D; _gcl_au=1.1.1889485766.1641589636; ppm_fpc=6577e6be-21c1-48c6-bd107852ebd9e58e; _rdt_uuid=1641589635747.214254d3-015d-4162-9f1c-6c5b14a5b5c3; gid=b991a0de-58ab-4270-b6f6-3b5c9d06ecfe; _gid=GA1.2.1906657857.1641589636; _dc_gtm_UA-16492313-1=1; _gat_UA-16492313-1=1; _scid=953cb2f2-af38-40de-bae4-7b6f6da68470; __pdst=3ea3aa9f76de471bb4d1ebbe20847d70; _uetsid=caa2bf306ffd11eca3241514665c14c5; _uetvid=caa2f2006ffd11ec9d7eef6a5b6bc9af; _hjSessionUser_26379=eyJpZCI6ImZhNzk2NDg1LThlYWYtNTFkYy05OGZkLWUxYzg3ODY0OWUxNiIsImNyZWF0ZWQiOjE2NDE1ODk2MzU5NjEsImV4aXN0aW5nIjpmYWxzZX0=; _hjFirstSeen=1; _hjSession_26379=eyJpZCI6IjdmODI3ZTg2LTc4YjgtNDNjZC1hZGIxLThjMzlhYzc3NWE3MSIsImNyZWF0ZWQiOjE2NDE1ODk2MzYxMTZ9; _hjIncludedInSessionSample=0; _hjAbsoluteSessionInProgress=0; _ga_MFT2R11DFX=GS1.1.1641589635.1.0.1641589635.0; _ga=GA1.1.1296695503.1641589636; _sctr=1|1641580200000; FPLC=PRQD3Si6ozjACGSu5m0Oy%2F2vBSfaBFs16ClzNd9W20V%2FlvqlwhnsFOOl5Z3hvOU6%2FfncwxZXl1wrBMG6JKkI3h2FNBzFr3fK%2FVyKA3VJBTiWIpCdCQ5ohVHJEsdpmg%3D%3D; FPID=FPID2.2.TfXPD1g87Yl3lb%2BhwF56gturH4xtQ5d6780PyWVonFs%3D.1641589636; __cf_bm=5x1Nm.LaSkkQYRUMnH6UbNSmtvykw8kkukE0XN_rRYw-1642881877-0-AT5f60gSRPo4y0dxLPnb4PJZh7buuXHD8PLQzSrK0jYUW5Tnp31G16cRyJQg67pdPl0TEMkr0k00PT0JWneGb0XIVDa5R5fsYOZZIoaC2V6/");
                return await client.ExecuteAsync(request);
            }
        }
        catch (HttpRequestException ex)
        {
            throw;
        }
    }
}