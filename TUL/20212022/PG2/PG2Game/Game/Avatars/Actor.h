//
// Created by Aliaksei Kalosha on 18.05.2022.
//

#ifndef PG2GAME_ACTOR_H
#define PG2GAME_ACTOR_H

class Actor: public SceneObject{
protected:

public:
    Actor(const glm::vec3 &pos, const glm::vec2 &angle) : SceneObject(pos, angle) {}

    void virtual update(const float &dt){}
};

class Flask : public Actor{
protected:
    float now;
public:

    Flask(const glm::vec3 &pos, const glm::vec2 &angle) : Actor(pos, angle) {
        now += static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
    }

    glm::vec3 getPos() override{
        return pos+glm::vec3(0, sin(now) * 0.1, 0);
    }

    void  update(const float &dt) override{
        now+=dt;
    }
};


#endif //PG2GAME_ACTOR_H
