// using UnityEngine;
//
//
// namespace Pokemonomania
// {
//     public class StoneBreakerAction : MonoBehaviour
//     {
//         [SerializeField] private PokemonController _controller;
//         [SerializeField] private int _breakerId;
//
//         public void BreakingTheStone()
//         {
//             if (_breakerId == _controller.CurrentId)
//             {
//                 _controller.Catch();
//                 ScoreService.Instance.AddScore();
//                 ComboService.Instance.AddCombo();
//             }
//             else
//             {
//                 ComboService.Instance.DropCombo();
//                 FailedMovementManager.instance.setFMBVisible(true);
//             }
//         }
//     }
// }