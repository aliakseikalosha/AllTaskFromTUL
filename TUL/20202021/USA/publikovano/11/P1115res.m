vstup=importdata('P1115.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);

%maximum x^7
modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2+b(4).*x.^3+b(5).*x.^4+b(6).*x.^5+b(7).*x.^6+b(8).*x.^7);
beta0=[0,0,0,0,0,0,0,0]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

%maximum x^6
modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2+b(4).*x.^3+b(5).*x.^4+b(6).*x.^5+b(7).*x.^6);
beta0=[0,0,0,0,0,0,0]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

%maximum x^5
modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2+b(4).*x.^3+b(5).*x.^4+b(6).*x.^5);
beta0=[0,0,0,0,0,0]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

%maximum x^4
modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2+b(4).*x.^3+b(5).*x.^4);
beta0=[0,0,0,0,0]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

%maximum x^3
modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2+b(4).*x.^3);
beta0=[0,0,0,0]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

