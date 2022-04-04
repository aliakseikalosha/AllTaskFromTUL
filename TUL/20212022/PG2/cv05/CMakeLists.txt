cmake_minimum_required(VERSION 3.21)
project(cv05)

set(CMAKE_CXX_STANDARD 17)

set(SOURCE_FILES main.cpp glInfo.h glerror.h glerror.cpp glInit.h shape.h input.h Avatars/Camera.h Avatars/Avatar.h Avatars/Player.h Game.h)
find_package(OpenGL REQUIRED)
find_package(OpenCV REQUIRED)
find_package(GLFW3 REQUIRED)
find_package(glm REQUIRED)

add_executable(cv05 ${SOURCE_FILES})
include_directories(${OpenCV_INCLUDE_DIRS})
target_link_libraries(cv05 ${OPENGL_LIBRARIES} glfw ${OpenCV_LIBS})