use reqwest::Client;
use std::error::Error;

#[tokio::main]
async fn main() -> Result<(), Box<dyn Error>> {
    // MES 系统的接口地址
    let mes_service_url = "http://172.18.3.67:8020/MesFrameWork.asmx";
//172.16.65.2 和172.16.65.5，端口为：8020 http://172.18.3.67:8020/MesFrameWork.asmx为测试端口
    // 构建 SOAP 请求
    let soap_request = build_soap_request();

    // 创建 HTTP 客户端
    let client = Client::new();

    // 发送 HTTP POST 请求
    let response = client
        .post(mes_service_url)
        .header("Content-Type", "text/xml")
        .header("SOAPAction", "http://tempuri.org/WS_EOL_DATA_UPLOAD")
        .body(soap_request)
        .send()
        .await?;

    // 检查响应状态
    if response.status().is_success() {
        // 读取响应内容
        let response_body = response.text().await?;
        println!("MES system response:\n{}", response_body);
    } else {
        eprintln!(
            "Failed to call MES service. Status code: {}",
            response.status()
        );
    }

    Ok(())
}

// 构建 SOAP 请求
fn build_soap_request() -> String {
    // 设备数据
    let data = MesData {
        m_machine_no: "Machine001".to_string(),
        m_workstation_sn: "WS001".to_string(),
        m_emp_no: "EMP001".to_string(),
        m_mo: "MO001".to_string(),
        m_operation: "Operation001".to_string(),
        m_satge: "Stage001".to_string(),
        m_test_mode: "TestMode001".to_string(),
        m_product_sn: "Product001".to_string(),
        m_cell_sn: "Cell001".to_string(),
        m_qty: "100".to_string(),
        m_ng_qty: "5".to_string(),
        m_result: "Pass".to_string(),
        m_error: "None".to_string(),
        m_error_qty: "0".to_string(),
        m_error_point: "None".to_string(),
        m_itemvalue: "ItemValue001".to_string(),
        m_voltage: "220V".to_string(),
        param1: "Param1Value".to_string(),
        param2: "Param2Value".to_string(),
    };

    // 构建 SOAP 请求体
    format!(
        r#"<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <WS_EOL_DATA_UPLOAD xmlns="http://tempuri.org/">
      <M_MACHINE_NO>{}</M_MACHINE_NO>
      <M_WORKSTATION_SN>{}</M_WORKSTATION_SN>
      <M_EMP_NO>{}</M_EMP_NO>
      <M_MO>{}</M_MO>
      <M_OPERATION>{}</M_OPERATION>
      <M_SATGE>{}</M_SATGE>
      <M_TEST_MODE>{}</M_TEST_MODE>
      <M_PRODUCT_SN>{}</M_PRODUCT_SN>
      <M_CELL_SN>{}</M_CELL_SN>
      <M_QTY>{}</M_QTY>
      <M_NG_QTY>{}</M_NG_QTY>
      <M_RESULT>{}</M_RESULT>
      <M_ERROR>{}</M_ERROR>
      <M_ERROR_QTY>{}</M_ERROR_QTY>
      <M_ERROR_POINT>{}</M_ERROR_POINT>
      <M_ITEMVALUE>{}</M_ITEMVALUE>
      <M_VOLTAGE>{}</M_VOLTAGE>
      <Param1>{}</Param1>
      <Param2>{}</Param2>
    </WS_EOL_DATA_UPLOAD>
  </soap:Body>
</soap:Envelope>"#,
        data.m_machine_no,
        data.m_workstation_sn,
        data.m_emp_no,
        data.m_mo,
        data.m_operation,
        data.m_satge,
        data.m_test_mode,
        data.m_product_sn,
        data.m_cell_sn,
        data.m_qty,
        data.m_ng_qty,
        data.m_result,
        data.m_error,
        data.m_error_qty,
        data.m_error_point,
        data.m_itemvalue,
        data.m_voltage,
        data.param1,
        data.param2
    )
}

// 设备数据结构
struct MesData {
    m_machine_no: String,
    m_workstation_sn: String,
    m_emp_no: String,
    m_mo: String,
    m_operation: String,
    m_satge: String,
    m_test_mode: String,
    m_product_sn: String,
    m_cell_sn: String,
    m_qty: String,
    m_ng_qty: String,
    m_result: String,
    m_error: String,
    m_error_qty: String,
    m_error_point: String,
    m_itemvalue: String,
    m_voltage: String,
    param1: String,
    param2: String,
}