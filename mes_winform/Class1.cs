using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MesWebServiceClient
{
    // MES 系统的接口地址
    private static readonly string MesServiceUrl = "http://172.18.3.67:8020/MesFrameWork.asmx";
    //172.16.65.2 和172.16.65.5，端口为：8020 http://172.18.3.67:8020/MesFrameWork.asmx为测试端口

    // 调用 MES 系统的 Web 服务
    public async Task<string> UploadDataToMesAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            // 构建 SOAP 请求
            string soapRequest = BuildSoapRequest();

            // 设置 HTTP 请求内容
            HttpContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/WS_EOL_DATA_UPLOAD");
           // richTextBox1
            // 发送 HTTP POST 请求
            HttpResponseMessage response = await client.PostAsync(MesServiceUrl, content);

            // 检查响应状态
            if (response.IsSuccessStatusCode)
            {
                // 读取响应内容
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to call MES service. Status code: {response.StatusCode}");
            }
        }
    }

    // 构建 SOAP 请求
    private string BuildSoapRequest()
    {
        // 设备数据
        var data = new
        {
            M_MACHINE_NO = "Machine001",
            M_WORKSTATION_SN = "WS001",
            M_EMP_NO = "000536",
            M_MO = "100291984A008",
            M_OPERATION = "00",
            M_SATGE = "N",
            M_TEST_MODE = "TestMode001",
            M_PRODUCT_SN = "Product001",
            M_CELL_SN = "Cell001",
            M_QTY = "100",
            M_NG_QTY = "5",
            M_RESULT = "Pass",
            M_ERROR = "A",
            M_ERROR_QTY = "1",
            M_ERROR_POINT = "None",
            M_ITEMVALUE = "ItemValue001",
            M_VOLTAGE = "220V",
            Param1 = "11",
            Param2 = "Param2Value"
        };

        // 构建 SOAP 请求体
        return $@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <WS_EOL_DATA_UPLOAD xmlns=""http://tempuri.org/"">
      <M_MACHINE_NO>{data.M_MACHINE_NO}</M_MACHINE_NO>
      <M_WORKSTATION_SN>{data.M_WORKSTATION_SN}</M_WORKSTATION_SN>
      <M_EMP_NO>{data.M_EMP_NO}</M_EMP_NO>
      <M_MO>{data.M_MO}</M_MO>
      <M_OPERATION>{data.M_OPERATION}</M_OPERATION>
      <M_SATGE>{data.M_SATGE}</M_SATGE>
      <M_TEST_MODE>{data.M_TEST_MODE}</M_TEST_MODE>
      <M_PRODUCT_SN>{data.M_PRODUCT_SN}</M_PRODUCT_SN>
      <M_CELL_SN>{data.M_CELL_SN}</M_CELL_SN>
      <M_QTY>{data.M_QTY}</M_QTY>
      <M_NG_QTY>{data.M_NG_QTY}</M_NG_QTY>
      <M_RESULT>{data.M_RESULT}</M_RESULT>
      <M_ERROR>{data.M_ERROR}</M_ERROR>
      <M_ERROR_QTY>{data.M_ERROR_QTY}</M_ERROR_QTY>
      <M_ERROR_POINT>{data.M_ERROR_POINT}</M_ERROR_POINT>
      <M_ITEMVALUE>{data.M_ITEMVALUE}</M_ITEMVALUE>
      <M_VOLTAGE>{data.M_VOLTAGE}</M_VOLTAGE>
      <Param1>{data.Param1}</Param1>
      <Param2>{data.Param2}</Param2>
    </WS_EOL_DATA_UPLOAD>
  </soap:Body>
</soap:Envelope>";
    }
}