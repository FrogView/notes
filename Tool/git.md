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
#### Git 打包文件
1. 打包所有文件
打包master分支的所有文件：
```
$ git archive --format=zip --output master.zip master
```
其中，输出格式为zip，输出文件为master.zip。git支持zip和tar两种输出格式。

打包当前分支当前HEAD的所有文件：
```
$ git archive --format=zip --output head.zip HEAD
```
打包v1.2标签的所有文件：
```
$ git archive --format=zip --output v1.2.zip v1.2
```
2. 打包更改的文件
打包更改文件的原理是：
用git diff 找出文件列表；
用打包命令打包。
也就是说，只要能用找出文件列表，就可以git打包出来。

* 打包最后修改的文件
先通过git diff找到最新版本修改过的文件，再压缩打包这些文件：
```
$ git archive --format=zip -o update.zip HEAD $(git diff --name-only HEAD^)
```
* 打包最后两个版本修改的文件
总共也是2个版本：
```
$ git archive --format=zip -o update.zip HEAD $(git diff --name-only HEAD~2)
```
* 打包两个分支之间差别的文件
```
$ git archive --format=zip -o update.zip HEAD $(git diff --name-only master fix-error)
```
如上，打包master和fix-error分支差异的文件。
