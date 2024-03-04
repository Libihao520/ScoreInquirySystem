# 数据推送

<br /> 

## 除治数据

<br /> 

### 推送方式

提供可被访问的Url，使用POST接收数据。(只推送已除治数据)

| Method | Content-Type | Content |
|  ---- | ---- | ---- |
| POST | application/json | body |

<br /> 

### 参数说明

| 参数名 | 数据类型 | 说明 |
|  ---- | ---- | ---- |
| id | string | Id唯一，支持幂等性 |
| no | string | 编号 |
| lng | numeric | 经度 |
| lat | numeric | 纬度 |
| xian_mc | string | 县名称 |
| xiang_mc | string | 乡名称 |
| cun_mc | string | 村名称 |
| lin_ban | string | 林班 |
| xiao_ban | string | 小班 |
| xiongjing | float8 | 胸径 |
| ms | string | 备注 |
| clean_time | string | 清理时间 |
| xzq_dm | int8 | 行政区代码 |
| is_deleted | bool | 是否删除 |
| clean_organization_name | string | 清理队伍名称 |
| cleaner_name | string | 清理人员名称 |
| audit_status | int | 乡级确认状态 |  
| xian_audit_status | int | 县级复核状态 | 
| clean_before_images | string[] | 清理前照片 | 
| clean_after_images | string[] | 清理后照片 | 
| nd | int | 年度，2023指2023/9至2024/9 | 

<br /> 
  
```json
[
    {
        "id":"",
        "no": "",
        "lng": 120.11,
        "lat": 23.00,
        "xian_mc": "县",
        "xiang_mc": "乡",
        "cun_mc": "村",
        "lin_ban": "林班",
        "xiao_ban": "小班",
        "xiongjing": 1,
        "ms": "",
        "cj_sj": 1698568376,
        "xzq_dm": 330000000000,
        "is_deleted": false,
        "clean_organization_name": "浙江**有限公司",
        "cleaner_name": "李四",
        "audit_status": 0,
        "xian_audit_status": "0",
        "clean_before_images":["http://****","http://****"],
        "clean_after_images":["http://****","http://****"],
        "nd": 2023,

    },
    ......
]
```

<br /> 

### 字段定义声明
| 参数名 | 值定义 | 
|  ---- | ---- | 
| 乡级确认状态 | 0 => 确认 ; 1 => 通过 ; 2 => 不通过 | 
| 县级县级复核状态 |  0 => 待复核 ; 1 => 通过 ; 2 => 不通过|


<br /> 
         
### 期待返回

<br /> 

#### 成功
```json
{
    "code": 200,
    "message": "",
    "success": true
}
```

<br /> 

#### 失败
```json
{
    "code": 500,
    "message": "",
    "success": false
}
```