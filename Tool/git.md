#### 使用[gitignore](https://gitignore.io/)自动生成.gitignore文件

1. 添加配置：

```
git config --global alias.ignore \
'!gi() { curl -sL https://www.gitignore.io/api/$@ ;}; gi'
```

2. 生成.gitignore文件(csharp)

```
git ignore csharp >> .gitignore
```

3. 查看所有支持的类型

```
git ignore list
```
