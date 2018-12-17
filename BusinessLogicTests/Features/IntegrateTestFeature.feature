Feature: IntegrateTestFeature
	In order to 
	As a 
	I want to 

Background: 
Given 設備清單為
	| DEVICE_SN | DEVICE_ID     | DEVICE_TYPE |
	| 2018001   | 192.168.10.99 | N           |
	| 2018002   | 192.168.10.98 | N           |
	| 2018003   | 192.168.60.87 | S           |
	| 2018004   | 10.2.253.5    | S           |
Given 監控項目資訊
	| DEVICE_SN | TARGET_NAME                     | TARGET_STATUS | OPERATOR_TYPE | IS_EXCEPTION |
	| 2018001   | Traffic - Gi1/0/20 [traffic_in] | 0             | Equal         | Y            |
	| 2018001   | Ping                            | 0             | In            | Y            |
	| 2018003   | EVENT_TYPE                      | 0             | Always        | Y            |
	| 2018003   | VIDEO		                      | 0             | Always        | Y            |
	| 2018004   | detect block ip                 | 0             | Always        | Y            |
Given 告警條件為
	| DEVICE_SN | TARGET_NAME                     | TARGET_VALUE |
	| 2018001   | Traffic - Gi1/0/20 [traffic_in] | ALERT        |
	| 2018001   | Ping                            | ERROR        |
	| 2018001   | Ping                            | DOWN         |
Given 前次監控訊息為
	| RECORD_SN     | DEVICE_SN | TARGET_NAME                     | IS_EXCEPTION |
	| 2018111200001 | 2018001   | Traffic - Gi1/0/20 [traffic_in] | N            |
	| 2018111200002 | 2018002   | Traffic - Gi1/0/20 [traffic_in] | Y            |
Given 通知條件為
	| DEVICE_SN | NOTIFICATION_TYPE | INTERVAL_LEVEL | INTERVAL_TIME |
	| 2018001   | 0                 | 0              | 2             |
	| 2018002   | 0                 | 1              | 0             |
	| 2018003   | 1                 | 1              | 0             |
	| 2018004   | 1                 | 2              | 1             |
Given 通知記錄為
	| RECORD_SN     | DEVICE_SN | TARGET_NAME                     | TARGET_MESSAGE                                  | NOTIFICATION_TIME   |
	| 2018111200001 | 2018002   | Traffic - Gi1/0/20 [traffic_in] | current value is 5630.6207                      | 2018/11/06 17:08:30 |
	| 2018111200001 | 2018004   | detect block ip                 | From 10.2.253.5 detect block ip 103.210.135.136 | 2018/8/8 16:14:20   |

Scenario: Cacti_ALERT訊息
	Given 偵測器"Cacti" 
	And 設備類型為"N"
	And 原始訊息為"{ "id":"192.168.10.99", "target": "Traffic - Gi1/0/20 [traffic_in]", "action":"ALERT", "info":"current value is 5630.6207","time":"2018/11/06 18:08:34" }"
	And 來源IP為""
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION |
	| 2018001   | 192.168.10.99 | Traffic - Gi1/0/20 [traffic_in] | ALERT        | current value is 5630.6207 | 2018/11/06 18:08:34 | Y            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018001   | 192.168.10.99 | Traffic - Gi1/0/20 [traffic_in] | ALERT        | current value is 5630.6207 | 2018/11/06 18:08:34 | Y            | Y               |

Scenario: Cacti_NORMAL訊息
	Given 偵測器"Cacti" 
	And 設備類型為"N"
	And 原始訊息為"{ "id":"192.168.10.98", "target": "Traffic - Gi1/0/20 [traffic_in]", "action":"NORMAL", "info":"current value is 5630.6207","time":"2018/11/06 18:08:34" }"
	And 來源IP為""
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION |
	| 2018002   | 192.168.10.98 | Traffic - Gi1/0/20 [traffic_in] | NORMAL       | current value is 5630.6207 | 2018/11/06 18:08:34 | N            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME                     | TARGET_VALUE | TARGET_MESSAGE             | RECEIVE_TIME        | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018002   | 192.168.10.98 | Traffic - Gi1/0/20 [traffic_in] | NORMAL       | current value is 5630.6207 | 2018/11/06 18:08:34 | N            | Y               |

Scenario: BobCacti_Error訊息
	Given 偵測器"BobCacti" 
	And 設備類型為"N"
	And 原始訊息為"{ "DEVICE_ID":"192.168.10.99", "ACTION_TYPE":"ERROR", "LOG_INFO":"ping down","LOG_TIME":"2018-09-13T13:21:30" }"
	And 來源IP為""
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME | TARGET_VALUE | TARGET_MESSAGE | RECEIVE_TIME        | IS_EXCEPTION |
	| 2018001   | 192.168.10.99 | Ping        | ERROR        | ping down      | 2018/09/13 13:21:30 | Y            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME | TARGET_VALUE | TARGET_MESSAGE | RECEIVE_TIME        | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018001   | 192.168.10.99 | Ping        | ERROR        | ping down      | 2018/09/13 13:21:30 | Y            | Y               |

Scenario: Camera發送告警
	Given 偵測器"Camera" 
	And 設備類型為"S"
	And 原始訊息為"EVENT_TYPE=Camera tampering detection&VIDEO=123"
	And 來源IP為"192.168.60.87"
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME | TARGET_VALUE               | TARGET_MESSAGE             | IS_EXCEPTION |
	| 2018003   | 192.168.60.87 | EVENT_TYPE  | Camera tampering detection | Camera tampering detection | Y            |
	| 2018003   | 192.168.60.87 | VIDEO       | 123                        | 123                        | Y            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID     | TARGET_NAME | TARGET_VALUE               | TARGET_MESSAGE             | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018003   | 192.168.60.87 | EVENT_TYPE  | Camera tampering detection | Camera tampering detection | Y            | Y               |
	| 2018003   | 192.168.60.87 | VIDEO       | 123                        | 123                        | Y            | Y               |


Scenario: Logmaster黑名單告警_通知
	Given 偵測器"Logmaster" 
	And 設備類型為"S"
	And 原始訊息為"{ "DEVICE_ID": "10.2.253.5", "LOG_INFO": "From 10.2.253.5 detect block ip 103.210.135.136", "LOG_TIME": "2018/8/8 16:20:10" }"
	And 來源IP為"10.2.253.5"
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID  | TARGET_NAME     | TARGET_VALUE    | TARGET_MESSAGE                                  | RECEIVE_TIME      | IS_EXCEPTION |
	| 2018004   | 10.2.253.5 | detect block ip | 103.210.135.136 | From 10.2.253.5 detect block ip 103.210.135.136 | 2018/8/8 16:20:10 | Y            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID  | TARGET_NAME     | TARGET_VALUE    | TARGET_MESSAGE                                  | RECEIVE_TIME      | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018004   | 10.2.253.5 | detect block ip | 103.210.135.136 | From 10.2.253.5 detect block ip 103.210.135.136 | 2018/8/8 16:20:10 | Y            | Y               |

Scenario: Logmaster黑名單告警_不通知
	Given 偵測器"Logmaster" 
	And 設備類型為"S"
	And 原始訊息為"{ "DEVICE_ID": "10.2.253.5", "LOG_INFO": "From 10.2.253.5 detect block ip 103.210.135.136", "LOG_TIME": "2018/8/8 16:14:30" }"
	And 來源IP為"10.2.253.5"
	When 執行EF告警作業
	Then EF解析告警結果為
	| DEVICE_SN | DEVICE_ID  | TARGET_NAME     | TARGET_VALUE    | TARGET_MESSAGE                                  | RECEIVE_TIME      | IS_EXCEPTION |
	| 2018004   | 10.2.253.5 | detect block ip | 103.210.135.136 | From 10.2.253.5 detect block ip 103.210.135.136 | 2018/8/8 16:14:30 | Y            |
	When 執行EF通知檢查作業
	Then EF通知檢查結果為
	| DEVICE_SN | DEVICE_ID  | TARGET_NAME     | TARGET_VALUE    | TARGET_MESSAGE                                  | RECEIVE_TIME      | IS_EXCEPTION | IS_NOTIFICATION |
	| 2018004   | 10.2.253.5 | detect block ip | 103.210.135.136 | From 10.2.253.5 detect block ip 103.210.135.136 | 2018/8/8 16:14:30 | Y            | N               |
