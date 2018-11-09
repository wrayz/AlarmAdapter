Feature: IntegrateTestFeature
	In order to 
	As a 
	I want to 

Background: 
Given 設備清單為
	| DEVICE_SN | DEVICE_ID     |
	| 2018001   | 192.168.10.99 |
Given 告警條件為
	| DEVICE_SN | TARGET_NAME                     | TARGET_VALUE | IS_EXCEPTION |
	| 2018001   | Traffic - Gi1/0/20 [traffic_in] | ALERT        | true         |

Scenario: Cacti_ALERT訊息
	Given 原始訊息為"{ "id":"192.168.10.99", "target": "Traffic - Gi1/0/20 [traffic_in]", "action":"ALERT", "info":"current value is 5630.6207","time":"2018/11/06 18:08:34" }"
	When 執行EF作業
	Then EF解析告警結果為
	| DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_CONTENT             | RECEIVE_TIME        | IS_EXCEPTION |
	| 192.168.10.99 | Traffic - Gi1/0/20 [traffic_in] | ALERT        | current value is 5630.6207 | 2018/11/06 18:08:34 | true         |
