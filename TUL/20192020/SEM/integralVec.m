function z = integralVec(x,y, od)
%if 1-exist('od')
%    od=1;
%end
z = zeros(1,length(x)-od);
for i = 1:length(x)
  xx =  x(1:i);
  yy = y(1:i);
  z(i) = integral(xx,yy);
end
end

