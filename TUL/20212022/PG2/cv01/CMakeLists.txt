cmake_minimum_required(VERSION 3.21)
project(cv01)

set(CMAKE_CXX_STANDARD 17)

set(SOURCE_FILES main.cpp glInfo.h)
find_package(OpenGL REQUIRED)
find_package(OpenCV REQUIRED)
find_package(GLFW3 REQUIRED)
find_package(glm REQUIRED)

add_executable(cv01 ${SOURCE_FILES})
include_directories(${OpenCV_INCLUDE_DIRS})
target_link_libraries(cv01 ${OPENGL_LIBRARIES} glfw ${OpenCV_LIBS})