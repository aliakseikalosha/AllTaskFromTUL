vstup=importdata('P1112.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);

%p��klad ad a
LM=LinearModel.fit(x,y,'Constant')

%p��klad ad b
modelfun=@(b,x)(b(1)./x+b(2));
beta0=[1,3]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)