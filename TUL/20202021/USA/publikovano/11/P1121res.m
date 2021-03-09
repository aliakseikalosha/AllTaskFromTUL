x=[1,2,3,4,5,6,7,8,9,10]; 
y=[0,5,9,18,28,39,69,111,177,277]; 

modelfun=@(b,x)(b(1).*x.^2+b(2).*x+b(3));
beta0=[1,1,1]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

modelfun1=@(b,x)(b(1).*x.^2+b(2).*x);
beta0=[1,1]
NLM1=NonLinearModel.fit(x,y,modelfun1,beta0)
