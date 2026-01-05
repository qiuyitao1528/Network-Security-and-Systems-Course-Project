# Network-Security-and-Systems-Course-Project



# 本地 Web 服务的安全暴露面控制与传输加密验证

## 项目简介

本项目基于 **ASP.NET Core Web API**，设计并实现了一个简单的本地 Web 服务，通过实验方式验证以下安全问题：

- 基于 API Key 的访问控制机制
- 不同监听地址配置对服务网络暴露面的影响
- HTTP 明文传输与 HTTPS（TLS）加密传输在安全性上的差异

项目主要用于 **网络安全 / 系统安全 / Web 安全相关课程实验与学习**，侧重实验可复现性与安全机制验证。

---

## 实验环境

- 操作系统：Windows
- 开发工具：Visual Studio
- 框架：ASP.NET Core
- 端口扫描工具：Nmap
- 抓包工具：Wireshark
- 测试工具：curl

---

## 功能说明

### 1. API Key 访问控制
- 服务提供 `/hello` 接口
- 客户端需在请求头中携带 `X-API-Key`
- 未授权访问返回 `401 Unauthorized`
- 授权成功返回 `200 OK` 与响应内容

> 注：API Key 写死在代码中，仅用于实验演示，生产环境应采用更安全的认证方式。

---

### 2. 服务监听地址与暴露面控制
通过配置 `ASPNETCORE_URLS` 或 Kestrel 监听地址，验证以下两种情况：

- 监听 `0.0.0.0`：服务端口对局域网可见，暴露面较大
- 监听 `127.0.0.1`：服务仅对本机可访问，降低网络暴露风险

并结合 Nmap 扫描结果进行验证分析。

---

### 3. HTTP 与 HTTPS 传输安全对比
- 在 HTTP 明文传输下，可通过抓包直接获取请求内容与 API Key
- 在 HTTPS 传输下，数据通过 TLS 加密，无法从抓包中获取应用层明文信息

---

## 快速运行

### 1. 启动服务（仅监听本机）

```bash
set ASPNETCORE_URLS=http://127.0.0.1:5146;https://127.0.0.1:7048
dotnet run
````

启动后控制台将显示监听地址。

---

### 2. 功能测试

**HTTP 请求：**

```bash
curl -i -H "X-API-Key: 123456" http://127.0.0.1:5146/hello
```

**HTTPS 请求：**

```bash
curl -k -i -H "X-API-Key: 123456" https://127.0.0.1:7048/hello
```

---

## 抓包验证

* HTTP 抓包过滤条件：

  ```text
  http && tcp.port == 5146
  ```
* HTTPS 抓包过滤条件：

  ```text
  tls && tcp.port == 7048
  ```

可在 Wireshark 中对比明文传输与加密传输的差异。

---

## 项目用途说明

本项目仅用于：

* 教学实验
* 课程设计
* 网络安全原理学习与验证

不建议直接用于生产环境。

---

## 参考资料

* Microsoft Docs：ASP.NET Core Kestrel
* RFC 8446：The Transport Layer Security (TLS) Protocol Version 1.3

---

## 作者

* 作者：邱逸涛
* 用途：课程实验 / 学习记录

```
