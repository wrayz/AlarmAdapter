Feature: IntegrateTestFeature
	In order to 
	As a 
	I want to 

Background: 
Given 設備清單為
	| DEVICE_SN | DEVICE_ID     | DEVICE_TYPE |
	| 2018001   | 192.168.10.99 | N           |
	| 2018002   | 192.168.10.98 | N           |
Given 告警條件為
	| DEVICE_SN | TARGET_NAME                     | TARGET_VALUE | IS_EXCEPTION |
	| 2018001   | Traffic - Gi1/0/20 [traffic_in] | ALERT        | Y            | 
Given 前次監控訊息為
	| RECORD_SN     | DEVICE_SN | TARGET_NAME                     | IS_EXCEPTION |
	| 2018111200001 | 2018001   | Traffic - Gi1/0/20 [traffic_in] | N            |
	| 2018111200002 | 2018002   | Traffic - Gi1/0/20 [traffic_in] | Y            |
Given 通知條件為
	| DEVICE_SN | NOTIFICATION_TYPE | MESSAGE_TYPE | MUTE_INTERVAL |
	| 2018001   | 0                 | 0            | 2             |
	| 2018002   | 0                 | 1            | 0             |
Given 通知記錄為
	| RECORD_SN     | DEVICE_SN | TARGET_NAME                     | TARGET_MESSAGE             | NOTIFICATION_TIME   |
	| 2018111200001 | 2018002   | Traffic - Gi1/0/20 [traffic_in] | current value is 5630.6207 | 2018/11/06 17:08:30 |

Scenario: Cacti_ALERT訊息
	Given 偵測器"Cacti" 
	And 設備類型為"N"
	And 原始訊息為"{ "id":"192.168.10.99", "target": "Traffic - Gi1/0/20 [traffic_in]", "action":"ALERT", "info":"current value is 5630.6207","time":"2018/11/06 18:08:34" }"
	When 執行EF作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION |
	| 2018001   | 192.168.10.99 | Traffic - Gi1/0/20 [traffic_in] | ALERT        | current value is 5630.6207 | 2018/11/06 18:08:34 | Y            |
	And EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018001   | 192.168.10.99 | Traffic - Gi1/0/20 [traffic_in] | ALERT        | current value is 5630.6207 | 2018/11/06 18:08:34 | Y            | Y               |

Scenario: Cacti_NORMAL訊息
	Given 偵測器"Cacti" 
	And 設備類型為"N"
	And 原始訊息為"{ "id":"192.168.10.98", "target": "Traffic - Gi1/0/20 [traffic_in]", "action":"NORMAL", "info":"current value is 5630.6207","time":"2018/11/06 18:08:34" }"
	When 執行EF作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION |
	| 2018002   | 192.168.10.98 | Traffic - Gi1/0/20 [traffic_in] | NORMAL       | current value is 5630.6207 | 2018/11/06 18:08:34 | N            |
	And EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018002   | 192.168.10.98 | Traffic - Gi1/0/20 [traffic_in] | NORMAL       | current value is 5630.6207 | 2018/11/06 18:08:34 | N            | Y               |
