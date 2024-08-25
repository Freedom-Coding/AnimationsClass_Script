using UnityEngine;

namespace LostFoxCub.Enemies
{
    public class Animation
    {
        public string name;
        public float length;

        public Animation(string _name, float _length = 0)
        {
            name = _name;
            length = _length;
        }
    }

    [System.Serializable]
    public class AnimationList
    {
        [SerializeField] private string[] animationNames;
        private Animation[] animations;
        private Animator animator;

        public void Initialize(Animator _animator)
        {
            animator = _animator;

            animations = new Animation[animationNames.Length];

            for (int i = 0; i < animationNames.Length; i++)
            {
                animations[i] = new Animation(animationNames[i]);
            }
        }

        private float GetAnimationLength(string stateName)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(stateName))
            {
                RuntimeAnimatorController controller = animator.runtimeAnimatorController;

                foreach (AnimationClip clip in controller.animationClips)
                {
                    if (Animator.StringToHash(clip.name) == stateInfo.shortNameHash)
                    {
                        return clip.length;
                    }
                }
            }

            return 0;
        }

        public void PlayRandomAnimation()
        {
            if (animations.Length == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, animations.Length);
            Animation animation = animations[randomIndex];

            if (animation.name != string.Empty)
            {
                animator.Play(animation.name);
            }
        }

        public bool TryPlayRandomAnimation()
        {
            if (animations.Length == 0)
            {
                return false;
            }

            int randomIndex = Random.Range(0, animations.Length);
            Animation animation = animations[randomIndex];

            if (animation.name != string.Empty)
            {
                animator.Play(animation.name);
                return true;
            }

            return false;
        }

        public void PlayRandomAnimation(out Animation animation)
        {
            animation = null;

            if (animations.Length == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, animations.Length);

            animation = animations[randomIndex];

            if (animation.name != string.Empty)
            {
                animator.Play(animation.name);

                if (animation.length == 0)
                {
                    animation.length = GetAnimationLength(animation.name);
                }
            }
        }

        public bool TryPlayRandomAnimation(out Animation animation)
        {
            animation = null;

            if (animations.Length == 0)
            {
                return false;
            }

            int randomIndex = Random.Range(0, animations.Length);

            animation = animations[randomIndex];

            if (animation.name != string.Empty)
            {
                animator.Play(animation.name);

                if (animation.length == 0)
                {
                    animation.length = GetAnimationLength(animation.name);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}