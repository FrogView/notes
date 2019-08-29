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

查看本地分支： git branch
查看远程分支：git branch -r
查看所有分支(包括远程和本地分支）： git branch -a