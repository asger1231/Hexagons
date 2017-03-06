using Hexes;
using NUnit.Framework;

public class OffsetTest {

	[Test]
	public void ConvertOffsetToCube() {
        //Arrange
        OffsetCoordinate offset = new OffsetCoordinate(2, 1, false, false);

        //Act
        CubeCoordinate cube = (CubeCoordinate)offset;

		//Assert
		Assert.AreEqual(2, cube.x);
		Assert.AreEqual(0, cube.y);
		Assert.AreEqual(-2, cube.z);
    }

    [Test]
    public void ConvertCubeToOffset() {
        //Arrange
        CubeCoordinate cube = new CubeCoordinate(-2, 1);

        //Act
        OffsetCoordinate offset = new OffsetCoordinate(cube, false, false); //Pointy |even

        //Assert
        Assert.AreEqual(-2, offset.column);
        Assert.AreEqual(-(0), offset.row);
    }
}
