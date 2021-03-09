x=[1,2,3,4,5,6,7,8,9,10];
y=[2,4,6,12,15,23,29,38,45,59];

modelfun=@(b,x)(b(1)+b(1).*x+b(1).*x.^2);
beta0=[1]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)