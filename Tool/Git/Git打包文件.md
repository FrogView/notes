#### 打包所有文件
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
#### 打包更改的文件
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
