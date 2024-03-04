# 学生信息查询

<br /> 

## 一、登录部分

<br /> 

### 1.获取图像验证码

获取base64格式的验证图形码，baseKey是绑定图形验证码的key，在登录请求的时候需要携带这个key，failureTime是图形码的失效时间

| Method | URL | Query Parameters |
|  ---- | ---- | ---- |
| Get | /api/Login/GraphValidateCode | Null |


### 返回值示例

#### 成功
```json
{
  "code": 0,
  "message": "请求成功！图形验证码有效期三分钟",
  "data": {
    "base64": "data:image/png;base64,iVBORw0KG*********",
    "baseKey": "e13866a0-8e68-4cce-bb35-574e664aab60",
    "failureTime": "2024-03-03T22:20:08.578249+08:00"
  }
}
```

### 2.登录获取Token

Token的有效期是10分钟
| Method | URL | Content-Type | Content |
|  ---- | ----| ---- | ---- |
| POST |/api/Login/GetToken |application/json | body |

<br /> 

### 参数说明

| 参数名 | 数据类型 | 说明 |
|  ---- | ---- | ---- |
| username | string | 用户名称（必填） |
| password | string | 密码（必填） |
| authcodeKey | string | 验证码key（必填） |
| authcode | string | 用户输入的验证信息（必填） |

<br /> 
  
```json
{
  "username": "lbh",
  "password": "123456",
  "authcodeKey": "e13866a0-8e68-4cce-bb35-574e664aab60",
  "authcode": "ABCD"
}
```

### 返回值示例

#### 成功
```json
{
  "code": 0,
  "message": "身份验证成功，返回token",
  "data": "token: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJOYW1lIjoibGJoIiwiZXhwIjoxNzA5NDc2NjE0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMTYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxMTYifQ.sNgk9x_RMrxIrgTE-GZl7Q11wY60igfw2rEmUwRbTVg"
}
```

#### 验证码失效
```json
{
  "code": 1,
  "message": "验证码已失效!",
  "data": null
}
```

#### 验证码错误
```json
{
  "code": 1,
  "message": "验证码错误!",
  "data": null
}
```

#### 失败
```json
{
  "code": 1,
  "message": "账号不存在，用户名或密码错误！",
  "data": null
}
```

## 二、获取数据部分（需要携带token）
未携带Token或Token过期时
Status Code: 401 Unauthorized

### 1.根据班级、姓名（模糊查询）、性别查询学生
| Method | URL | Content-Type | Content |
|  ---- | ----| ---- | ---- |
| POST |/api/GetData/getStudentData |application/json | body |

### 参数说明

| 参数名 | 数据类型 | 说明 |
|  ---- | ---- | ---- |
| pageIndex | int | 当前页数（默认第一页） |
| pageSize | int | 条数（默认十条） |
| class | string | 班级 |
| name | string | 姓名（支持模糊查询） |
| sex | bool | 性别 |

### 返回值示例

#### 成功
```json
{
  "code": 0,
  "message": "成功",
  "data": {
    "total": 3,
    "data": [
      {
        "name": "张三",
        "sex": false,
        "status": 0
      },
      {
        "name": "王五",
        "sex": false,
        "status": 2
      }
    ]
  }
}
```

#### 没有数据
```json
{
  "code": 0,
  "message": "没有数据",
  "data": {
    "total": 0,
    "data": null
  }
}
```

### 2.根据姓名查询成绩
| Method | URL | Query Parameters |
|  ---- | ---- | ---- |
| Get | /api/GetData/GetPerformance | ?name="张三" |

### 返回值示例

#### 成功
```json
{
  "code": 0,
  "message": "成绩！",
  "data": [
    {
      "id": 1,
      "name": "张三",
      "examination": "期中考试",
      "subjectName": "数学",
      "value": 34
    },
    {
      "id": 31,
      "name": "张三",
      "examination": "期中考试",
      "subjectName": "语文",
      "value": 81
    }
    .....
  ]
}
```

#### 学生不存在
```json
{
  "code": 1,
  "message": "该学生不存在！",
  "data": null
}
```

### 3.最近一次考试每个班各科的平均分，按平均分降序排序
| Method | URL | Query Parameters |
|  ---- | ---- | ---- |
| Get | /api/GetData/GetScoreRanking | null |

### 返回值示例

#### 成功
```json
{
  "code": 0,
  "message": "最近一次考试的成绩！",
  "data": [
    {
      "班级名称": "一班",
      "学生id": 23,
      "姓名": "施二十五",
      "语文": 54,
      "数学": 64,
      "英语": 87,
      "总分": 205,
      "cou": 1
    },
    {
      "班级名称": "一班",
      "学生id": 16,
      "姓名": "杨十八",
      "语文": 82,
      "数学": 21,
      "英语": 43,
      "总分": 146,
      "cou": 2
    },
    {
      "班级名称": "一班",
      "学生id": 1,
      "姓名": "张三",
      "语文": 74,
      "数学": 36,
      "英语": 26,
      "总分": 136,
      "cou": 3
    },
    ........
```