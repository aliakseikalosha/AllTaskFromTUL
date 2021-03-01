x=[380, 402, 408, 412, 454, 459, 472, 481, 491, 522];

normplot(x)

mu=mean(x);
smerodch=std(x);

[h,p,ci]=ttest(x,500)
[h,p,ci]=ttest(x,500,0.05,'left')
[h,p,ci]=ttest(x,500,0.05,'right')