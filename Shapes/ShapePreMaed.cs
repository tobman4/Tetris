using System;
namespace Tetris.Shapes {
    public static class ShapePreMaed {

        /// <summary>
        /// Shape[shape,rot,y,x]
        /// </summary>

        [Obsolete]
        public static bool[,,,] Shape = new bool[,,,] {
            {
                #region Line
                {
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                },
                {
                    { true,true,true,true },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                },
                {
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                },
                {
                    { true,true,true,true },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                }
                #endregion
            }, {
                
                #region BOX
                {
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                },
                {
                    { true,true,true,true },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                },
                {
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                    { false,true,false,false },
                },
                {
                    { true,true,true,true },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                    { false,false,false,false },
                }
                #endregion
            }
        };
    }
}
