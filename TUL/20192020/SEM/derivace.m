function z = derivace(x,y)
for i = 2:length(x)-1
    z(i)= (y(i+1) - y(i-1))/(x(i+1)-x(i-1));
end
end

