import Countdown from 'react-countdown';

const renderer = ({
    days,
    hours,
    minutes,
    seconds,
    completed,
}: {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
    completed: boolean;
}) => {
    return (
        <div
            className={`
        border-2 border-white text-white py-1 px-2 flex justify-center rounded-lg
        ${completed ? 'bg-amber-600' : days === 0 && hours < 10 ? 'bg-red-600' : 'bg-green-600'}
      `}
        >
            {completed ? (
                <span>Auction Finished</span>
            ) : (
                <span>
                    {days}d:{hours}h:{minutes}m:{seconds}s
                </span>
            )}
        </div>
    );
};


type Props = {
    auctionEnd: string
}

export default function CountDownTimer({ auctionEnd }: Props) {
    return (
        <>
            <div>
                <Countdown date={auctionEnd} renderer={renderer} />
            </div>
        </>
    )
}

