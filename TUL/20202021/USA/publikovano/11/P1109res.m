x=[1,2,3,4,5,6,7,8,9,10]'; 
y=[1,2,3,1,2,3,1,2,3,1]'; 
z=[3,9,17,10,16,26,14,25,38,23]'; 

LM1=LinearModel.fit([x,y],z,'linear')
LM2=LinearModel.fit([x,y],z,'interactions')