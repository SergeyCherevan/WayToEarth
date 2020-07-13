using System.Collections.Generic;

namespace WayToEarth
{
    public class MessageTurn
    {
        virtual public List<KeyValuePair<Message, object>> mesTurn { get; set; }

        public MessageTurn() {
            if (this.GetType() == typeof(MessageTurn))
                mesTurn = new List<KeyValuePair<Message, object>>();
        }

        virtual public MessageTurn push(Message code, object inform)
        {
            mesTurn.Add(new KeyValuePair<Message, object>(code, inform));

            return this;
        }

        virtual public MessageTurn push(KeyValuePair<Message, object> mes)
        {
            mesTurn.Add(mes);

            return this;
        }

        virtual public KeyValuePair<Message, object> pop()
        {
            KeyValuePair<Message, object> ret;

            if (mesTurn.Count > 0)
            {
                ret = mesTurn[0];
                mesTurn.RemoveRange(0, 1);
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }

        virtual public KeyValuePair<Message, object> get()
        {
            KeyValuePair<Message, object> ret;

            if (mesTurn.Count > 0)
            {
                ret = mesTurn[0];
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }

        virtual public KeyValuePair<Message, object> popByCode(Message code)
        {
            KeyValuePair<Message, object> ret;

            int ind = mesTurn.FindIndex(
                    delegate (KeyValuePair<Message, object> mes)
                    {
                        return mes.Key == code;
                    }
                );


            if (ind != -1)
            {
                ret = mesTurn[ind];
                mesTurn.RemoveRange(ind, 1);
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }

        virtual public KeyValuePair<Message, object> getByCode(Message code)
        {
            KeyValuePair<Message, object> ret;

            int ind = mesTurn.FindIndex(
                    delegate (KeyValuePair<Message, object> mes)
                    {
                        return mes.Key == code;
                    }
                );


            if (ind != -1)
            {
                ret = mesTurn[ind];
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }

        virtual public KeyValuePair<Message, object> popByCodes(List<Message> codes)
        {
            KeyValuePair<Message, object> ret;

            int ind = mesTurn.FindIndex(
                    delegate (KeyValuePair<Message, object> mes)
                    {
                        return codes.Contains(mes.Key);
                    }
                );


            if (ind != -1)
            {
                ret = mesTurn[ind];
                mesTurn.RemoveRange(ind, 1);
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }

        virtual public KeyValuePair<Message, object> getByCodes(List<Message> codes)
        {
            KeyValuePair<Message, object> ret;

            int ind = mesTurn.FindIndex(
                    delegate (KeyValuePair<Message, object> mes)
                    {
                        return codes.Contains(mes.Key);
                    }
                );


            if (ind != -1)
            {
                ret = mesTurn[ind];
            }
            else
            {
                ret = new KeyValuePair<Message, object>(Message.EmptyTurn, null);
            }

            return ret;
        }
    }
}
