'use client';
import React from 'react'
import CountDownTimer from './CountDownTimer';
import CarImage from './CarImage';
import { Auction } from '@/types';


type Props = {
    auction: Auction
}

export default function AuctionCard({ auction }: Props) {
    return (
        <a>
            <div className='relative w-full bg-gray-200 aspect-video rounded-lg overflow-hidden'>
                <CarImage auctionImageUrl={auction.imageUrl} />
                <div className="absolute bottom-2 left-2">
                    <CountDownTimer auctionEnd={auction.auctionEnd} />
                </div>
            </div>

            <div className='flex justify-between mt-2'>
                <div>{auction.make}-{auction.model}</div>
                <div>{auction.year}</div>
            </div>
        </a>
    )
}
