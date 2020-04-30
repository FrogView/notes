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
#### Git 分支操作
* 查看本地分支： git branch
* 查看远程分支：git branch -r
* 查看所有分支(包括远程和本地分支）： git branch -a
* 创建分支
  * 本地创建分支: git branch branch_name
  * 远程创建一个和本地同名的分支: git push --set-upstream origin branch_name
  * 本地创建一个和远程同名的分支: git checkout --track origin/branch_name
* 合并分支（将 iss53 合并到 master ）
  $ git checkout master
  $ git merge iss53
#### Git pull 强制拉取并覆盖本地代码
```
git fetch --all
git reset --hard origin/master
git pull
```
#### [Git 打包文件](Git/Git打包文件.md)

