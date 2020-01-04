namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private float m_MaxEnergyAmount;
        private float m_CurrentEnergyAmount;

        public Engine(float i_MaxEnergyAmount, float i_CurrentEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        protected float MaxEnergyAmount
        {
            get { return m_MaxEnergyAmount; }
        }

        protected float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set { m_CurrentEnergyAmount = value; }
        }

        public float EnergyLeftPrecentage
        {
            get { return (this.m_CurrentEnergyAmount / this.m_MaxEnergyAmount) * 100; }
        }

        public override string ToString()
        {
            return string.Format("Current energy amount: {0}. Maximal energy amount: {1}", m_CurrentEnergyAmount, MaxEnergyAmount);
        }
    }
}
