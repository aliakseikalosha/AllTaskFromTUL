x=[1,2,3,4,5,6,7,8,9,10,1,2,3,4,5,6,7,8,9,10]; 
y=[1,2,3,4,5,6,7,8,9,10,1,4,9,16,25,36,49,64,81,100];

modelfun1=@(b,x)(b(1).*x);
beta0=[1]
NLM1=NonLinearModel.fit(x,y,modelfun1,beta0)

modelfun2=@(b,x)(b(1).*x.^2+b(2).*x);
beta0=[1,1]
NLM2=NonLinearModel.fit(x,y,modelfun2,beta0)

modelfun3=@(b,x)(b(1).*x.^2);
beta0=[1]
NLM3=NonLinearModel.fit(x,y,modelfun3,beta0)