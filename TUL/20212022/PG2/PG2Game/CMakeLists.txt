cmake_minimum_required(VERSION 3.21)
project(PG2Game)

set(CMAKE_CXX_STANDARD 17)

set(SOURCE_FILES main.cpp GL/glInfo.h GL/glerror.h GL/glerror.cpp GL/glInit.h GL/shape.h GL/input.h Game/Avatars/Camera.h Game/Avatars/Avatar.h Game/Avatars/Player.h Game/Game.h Game/Renderer/MeshRenderer.h GL/Mesh.h Game/Avatars/SceneObject.h DataLoader/texture.h DataLoader/texture.cpp GL/Vertex.h Game/Data/LightData.h Game/Avatars/Light.h Game/Renderer/LightRenderer.h Game/Avatars/Actor.h Game/Renderer/ActorRenderer.h Game/Data/SpriteData.h)
find_package(OpenGL REQUIRED)
find_package(OpenCV REQUIRED)
find_package(GLFW3 REQUIRED)
find_package(glm REQUIRED)

add_executable(PG2Game ${SOURCE_FILES})
include_directories(${OpenCV_INCLUDE_DIRS})
target_link_libraries(PG2Game ${OPENGL_LIBRARIES} glfw ${OpenCV_LIBS})