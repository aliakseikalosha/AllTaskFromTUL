vstup=importdata('P1117.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);
z=vstup.data.List1(:,3);

X=[x,y];

modelfun=@(b,X)(b(1)./x+b(2)./y+b(3)./(x+y));
beta0=[2,4,1]
NLM=NonLinearModel.fit(X,z,modelfun,beta0)

modelfun=@(b,X)(b(1)./y+b(2)./(x+y));
beta0=[4,1]
NLM=NonLinearModel.fit(X,z,modelfun,beta0)