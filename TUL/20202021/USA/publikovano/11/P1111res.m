x=[1,2,3,5,7,4,1,2,3,4,1,2,2,2,3]';
y=[2,1,1,4,1,2,5,2,1,2,1,2,1,2,3]';
z=[8,11,14,18,12,18,15,15,15,12,1,11,8,7,5]';

LM1=LinearModel.fit([x,y],z,'linear')
LM2=LinearModel.fit([x,y],z,'interactions')