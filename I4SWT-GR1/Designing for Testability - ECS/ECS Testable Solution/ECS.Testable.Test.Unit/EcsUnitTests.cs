using NUnit.Framework;

namespace ECS.Testable.Test.Unit
{
    [TestFixture]
    public class EcsUnitTests
    {
        private ECS _uut;

        [SetUp]
        public void SetUpUut()
        {
            _uut = new ECS(30);
        }

        // Set/GetThreshold

        [Test]
        public void SetThreshold_ToTen_GetThresholdReturnsTen()
        {
            _uut.SetThreshold(10);
            Assert.That(_uut.GetThreshold(), Is.EqualTo(10));
        }

        // GetCurTemp

        [Test]
        public void GetCurTemp_StupSensorSetToThirtyDegrees_ReturnsThirty()
        {
            var sensor = new StubTempSensor(true, 30);
            _uut.SetTempSensor(sensor);
            Assert.That(_uut.GetCurTemp(), Is.EqualTo(30));
        }

        // Regulate

        [TestCase(10, 30)]
        [TestCase(30, 10)]
        public void Regulate_CurTempAboveOrBelowThreshold_NoExceptionsThrown(int curTemp, int threshold)
        {
            var heater = new StubHeater();
            var sensor = new StubTempSensor(true, curTemp);
            _uut.SetHeater(heater);
            _uut.SetTempSensor(sensor);
            _uut.SetThreshold(threshold);
            Assert.DoesNotThrow(() =>
            {
                _uut.Regulate();
            });
        }

        // RunSelfTest

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void RunSelfTest_HeaterAndSensorIsWorking_ReturnsTrueIfBothAreWorking(bool heaterIsWorking,
            bool sensorIsWorking)
        {
            var heater = new StubHeater(heaterIsWorking);
            var sensor = new StubTempSensor(sensorIsWorking);
            _uut.SetHeater(heater);
            _uut.SetTempSensor(sensor);
            Assert.That(_uut.RunSelfTest(), Is.EqualTo(heaterIsWorking && sensorIsWorking));
        }
    }
}
