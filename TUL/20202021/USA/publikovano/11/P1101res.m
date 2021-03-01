x=[3,5,8,11,12,14,15];
y=[6,11,15,22,25,27,30];
LM=fitlm(x,y)

modelfun=@(b,x)b(1)*x;
beta0=(2);
NLM=fitnlm(x,y,modelfun,beta0)