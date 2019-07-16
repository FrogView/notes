[Hexo的Next主题详细配置](https://www.jianshu.com/p/3a05351a37dc)

[Hexo NexT主题中集成gitalk评论系统](https://asdfv1929.github.io/2018/01/20/gitalk/)

gitalk config:
```
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/gitalk@1/dist/gitalk.css">
<script src="https://cdn.jsdelivr.net/npm/gitalk@1/dist/gitalk.min.js"></script>
  
<div id="gitalk-container"></div>cript>

const gitalk = new Gitalk({
  clientID: '',
  clientSecret: '',
  repo: 'FrogView.github.io',
  owner: 'FrogView',
  admin: ['FrogView'],
  id: location.Blog.Comments,      // Ensure uniqueness and length less than 50
  distractionFreeMode: false  // Facebook-like distraction free mode
})
```

